using Common.Application;
using Domain.OrderAgg;
using Domain.OrderAgg.Interfaces.Repository;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Order.CreateOrder
{
    public class CreateOrderCommand : IBaseCommand
    {
        public Guid userId { get; set; }
    }
    public sealed class CreateOrderCommandHandler : IBaseCommandHandler<CreateOrderCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IUserRepository repository, IOrderRepository orderRepository)
        {
            _repository = repository;
            _orderRepository = orderRepository;
        }

        public async Task<OperationResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync(request.userId);
            if (user == null)
                return OperationResult.NotFound();

            var order = new Domain.OrderAgg.Order(request.userId);
            await _orderRepository.SaveChangeAsync();
            return OperationResult.Success();   
        }
    }
}
