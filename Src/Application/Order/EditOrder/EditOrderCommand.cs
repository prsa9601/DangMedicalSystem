using Common.Application;
using Domain.OrderAgg.Interfaces.Repository;
using Domain.UserAgg.Interfaces.Repository;
using System.Reflection.Metadata.Ecma335;

namespace Application.Order.EditOrder
{
    public class EditOrderCommand : IBaseCommand
    {
        public Guid orderId { get; set; }
        public Guid userId { get; set; }
    }
    public sealed class EditOrderCommandHandler : IBaseCommandHandler<EditOrderCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IOrderRepository _orderRepository;

        public EditOrderCommandHandler(IUserRepository repository, IOrderRepository orderRepository)
        {
            _repository = repository;
            _orderRepository = orderRepository;
        }

        public async Task<OperationResult> Handle(EditOrderCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync(request.userId);
            if (user == null)
                return OperationResult.NotFound();

            var order = await _orderRepository.GetTracking(request.orderId);
            await _orderRepository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
