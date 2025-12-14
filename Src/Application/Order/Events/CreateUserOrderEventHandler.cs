using Domain.OrderAgg;
using Domain.OrderAgg.Interfaces.Repository;
using Domain.UserAgg.Events;
using MediatR;

namespace Application.Order.Events
{
    internal class CreateUserOrderEventHandler : INotificationHandler<CreateOrderEvent>
    {
        private readonly IOrderRepository _repository;

        public CreateUserOrderEventHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateOrderEvent notification, CancellationToken cancellationToken)
        {
            var order = new Domain.OrderAgg.Order(notification.UserId);
            await _repository.AddAsync(order);

            await _repository.SaveChangeAsync();
        }
    }
}
