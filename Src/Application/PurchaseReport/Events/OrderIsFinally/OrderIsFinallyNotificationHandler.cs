using AngleSharp.Io;
using Domain.OrderAgg.Interfaces.Event;
using Domain.OrderAgg.Interfaces.Repository;
using Domain.ProductAgg.Interfaces.Repository;
using Domain.PurchaseReportAgg;
using Domain.PurchaseReportAgg.Interfaces.Repository;
using Domain.UserAgg.Interfaces.Repository;
using MediatR;

namespace Application.PurchaseReport.Events.OrderIsFinally
{
    public class OrderIsFinallyNotificationHandler : INotificationHandler<OrderEvent>
    {
        private readonly IOrderRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseReportRepository _purchaseReportRepository;

        public OrderIsFinallyNotificationHandler(IOrderRepository repository, IUserRepository userRepository, IProductRepository productRepository, IPurchaseReportRepository purchaseReportRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _purchaseReportRepository = purchaseReportRepository;
        }

        public async Task Handle(OrderEvent notification, CancellationToken cancellationToken)
        {
            var order = await _repository.GetTracking(notification.OrderId);
            if (order == null)
                return;

            var user = await _userRepository.GetTracking(order.UserId);
            if (user == null)
                return;


            List<Domain.PurchaseReportAgg.PurchaseReport> purchaseReports = new();
            foreach (var item in order.OrderItems)
            {
                var product = await _productRepository.GetByFilterAsync(i => i.Id.Equals(item.ProductId));
                if (product == null)
                    return;
                string profit = null;
                if (decimal.TryParse(product.Inventory.Profit, out var inventoryProfit))
                {

                    profit = (item.DongAmount * inventoryProfit).ToString();
                }
                purchaseReports.Add(new Domain.PurchaseReportAgg.PurchaseReport(user.Id, item.ProductId, product.Inventory.TotalPrice,
                    product.Inventory.Dong.ToString(), profit, item.TotalPrice.ToString(), item.DongAmount, product.Inventory.Profit,
                    product.Inventory.Profit, product.Inventory.PricePerDong, product.Inventory.Dong
                    ));

            }
            var orderEvent = order.OrderItems.FirstOrDefault(i => i.OrderId.Equals(notification.OrderId));
            await _purchaseReportRepository.AddRange(purchaseReports);
            await _purchaseReportRepository.SaveChangeAsync();
            order.OrderItems.Remove(orderEvent);

        }
    }
}
