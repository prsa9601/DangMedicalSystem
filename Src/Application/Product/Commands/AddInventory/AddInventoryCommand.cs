using Common.Application;
using Domain.ProductAgg.Enum;
using Domain.ProductAgg.Interfaces.Repository;

namespace Application.Product.Commands.AddInventory
{
    public class AddInventoryCommand : IBaseCommand
    {
        public string totalPrice { get; set; }
        public int dong { get; set; }
        public string profit { get; set; }
        public Guid productId { get; set; }
        public PaymentTime? paymentTime { get; set; }
    }
    public sealed class AddInventoryCommandHandler : IBaseCommandHandler<AddInventoryCommand>
    {
        private readonly IProductRepository _repository;

        public AddInventoryCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(request.productId);
            if (product is null)
            {
                return OperationResult.NotFound();
            }

            product.SetInventory(request.totalPrice, request.dong, request.profit);

            if (request.paymentTime is not null)
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
