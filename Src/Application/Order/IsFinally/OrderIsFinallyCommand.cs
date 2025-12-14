using Common.Application;
using Domain.OrderAgg.Interfaces.Event;
using Domain.OrderAgg.Interfaces.Repository;
using Domain.OrderAgg.Interfaces.Services;
using Domain.ProductAgg.Interfaces.Repository;
using Domain.ProfitAgg.Repository;
using Domain.UserAgg.Events;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Order.IsFinally
{
    public class OrderIsFinallyCommand : IBaseCommand
    {
        public Guid orderId { get; set; }
        public Guid userId { get; set; }
    }
    public sealed class OrderIsFinallyCommandHandler : IBaseCommandHandler<OrderIsFinallyCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderDomainService _service;

        public OrderIsFinallyCommandHandler(IOrderRepository orderRepository, 
            IUserRepository repository, IOrderDomainService service, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _repository = repository;
            _service = service;
            _productRepository = productRepository;
        }

        public async Task<OperationResult> Handle(OrderIsFinallyCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync(request.userId);
            if (user == null)
                return OperationResult.NotFound();

            if (!(user.UserDocument.Status == Domain.UserAgg.Enum.UserDocumentStatus.IsConfirmed))
            {
                return OperationResult.Error("ابندا باید اطلاعات هویتی خود را ارسال کنید و اطلاعات شما توسط مسئول مربوطه تایید شود.");
            }

            if (!(user.BankAccount.IsConfirmed == true))
            {
                return OperationResult.Error("ابندا باید اطلاعات بانکی خود را ارسال کنید و اطلاعات شما توسط مسئول مربوطه تایید شود.");
            }

            var order = await _orderRepository.GetTracking(request.orderId);
            
            var product = await _productRepository.GetTracking(order.OrderItems.ProductId);
                if (product == null) return OperationResult.NotFound();

            if (product.Status == Domain.ProductAgg.Enum.ProductStatus.IsDone)
            {
                return OperationResult.Error("این محصول قابل خریداری نیست.");
            }


            if (!await _service.CanPurchase(order.OrderItems.ProductId))
            {
               
                product.ChangeStatus(Domain.ProductAgg.Enum.ProductStatus.IsDone);

                await _orderRepository.SaveChangeAsync();
                return OperationResult.Error("6 دانگ این محصول خریداری شده است.");
            }
            
            
            if (order.status == Domain.OrderAgg.Enum.OrderStatus.paid)
            {
                return OperationResult.Error("مبلغ یکبار پرداخت شده.");
            }
            order.IsPaid();
            order.AddDomainEvent(new OrderEvent(order.Id));

            user.AddDomainEvent(new CreateOrderEvent()
            {
                UserId = user.Id,
            });
            await _orderRepository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
