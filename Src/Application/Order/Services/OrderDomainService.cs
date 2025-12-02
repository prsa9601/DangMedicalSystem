using Domain.OrderAgg.Interfaces.Repository;
using Domain.OrderAgg.Interfaces.Services;

namespace Application.Order.Services
{
    internal class OrderDomainService : IOrderDomainService
    {
        private readonly IOrderRepository _repository;

        public OrderDomainService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CanPurchase(Guid productId)
        {
            var order = await _repository.GetListByFilterAsync(order =>
             order.OrderItems
           .ProductId.Equals(productId) && order.status == Domain.OrderAgg.Enum.OrderStatus.paid);

            return order.Sum(i => i.OrderItems.DongAmount) >= 6 ? false : true;
        }

        public async Task<int> CheckNumberOfDongAvailable(Guid productId)
        {
            var order = await _repository.GetListByFilterAsync(order =>
            order.OrderItems
            .ProductId.Equals(productId) && order.status == Domain.OrderAgg.Enum.OrderStatus.paid);

            //var order = await _repository.GetListByFilterAsync(order =>
            //order.OrderItems.Any(item =>
            //item.ProductId.Equals(productId)) && order.status == Domain.OrderAgg.Enum.OrderStatus.paid);

            int dongCount = 0;
            //dongCount += order.Sum(o => o.OrderItems.Sum(i => i.DongAmount));
            dongCount += order.Sum(o => o.OrderItems.DongAmount);
            return 6 - dongCount;
        }
    }
}
