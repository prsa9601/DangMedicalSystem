using Common.Application;
using Domain.OrderAgg.Interfaces.Repository;
using Domain.ProductAgg.Interfaces.Repository;
using MediatR;

namespace Application.Order.SetOrderItem
{
    public class SetOrderItemCommand : IBaseCommand
    {
        public Guid orderId { get; set; }
        public Guid userId { get; set; }
        public Guid productId { get; set; }
        public int dongAmount { get; set; }
    }
    internal sealed class SetOrderItemCommandHandler : IBaseCommandHandler<SetOrderItemCommand>
    {
        private readonly IOrderRepository _repository;
        private readonly IProductRepository _productRepository;

        public SetOrderItemCommandHandler(IOrderRepository repository, IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public async Task<OperationResult> Handle(SetOrderItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByFilterAsync(i => i.Id.Equals(request.orderId)
            && i.UserId.Equals(request.userId));

            var product = await _productRepository.GetTracking(request.productId);

            if (order == null) return OperationResult.NotFound();

            if (product == null) return OperationResult.NotFound();

            order.SetOrderItem(request.productId, product.Inventory.PricePerDong ?? default, request.dongAmount, product.Inventory.Id);

            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
