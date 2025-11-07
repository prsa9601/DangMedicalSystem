using Common.Application;
using Domain.OrderAgg.Interfaces.Event;
using Domain.OrderAgg.Interfaces.Repository;
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

        public OrderIsFinallyCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OperationResult> Handle(OrderIsFinallyCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync(request.userId);
            if (user == null)
                return OperationResult.NotFound();

            var order = await _orderRepository.GetTracking(request.orderId);
            order.IsPaid();
            order.AddDomainEvent(new OrderEvent(order.Id));

            await _orderRepository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
