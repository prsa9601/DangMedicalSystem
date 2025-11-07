using Common.Application;
using Domain.ProductAgg.Enum;
using Domain.ProductAgg.Interfaces.Repository;

namespace Application.Product.Commands.SetProfitableTime
{
    public class SetInventoryProfitableCommand : IBaseCommand
    {
        public Guid productId { get; set; }
        public PaymentTime paymentTime { get; set; }
    }
    public sealed class SetInventoryProfitableCommandHandler : IBaseCommandHandler<SetInventoryProfitableCommand>
    {
        private readonly IProductRepository _repository;

        public SetInventoryProfitableCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(SetInventoryProfitableCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(request.productId);
            if (product is null)
            {
                return OperationResult.NotFound();
            }

            product.SetInventoryProfitableTime(request.paymentTime switch
            {
                PaymentTime.سالانه => PaymentTime.سالانه,
                PaymentTime.شش_ماهه => PaymentTime.شش_ماهه,
                PaymentTime.سه_ماهه => PaymentTime.سه_ماهه,
                PaymentTime.ماهانه => PaymentTime.ماهانه,
            });

            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
