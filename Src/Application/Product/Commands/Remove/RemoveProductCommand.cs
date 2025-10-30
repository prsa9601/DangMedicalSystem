using Common.Application;
using Domain.ProductAgg.Interfaces.Repository;
using Domain.ProductAgg.Interfaces.Services;

namespace Application.Product.Commands.Remove
{
    public class RemoveProductCommand : IBaseCommand
    {
        public Guid productId { get; set; }
    }
    internal sealed class RemoveProductCommandHandler : IBaseCommandHandler<RemoveProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IProductDomainService _service;

        public RemoveProductCommandHandler(IProductRepository repository, IProductDomainService service)
        {
            _repository = repository;
            _service = service;
        }
        //باید جوری زده شه اگه دنگش مال کسی بود پولش پرداخت بشه. بعد حذف بشه
        public async Task<OperationResult> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetTracking(request.productId);
            if (product == null) return OperationResult.NotFound("محصولی یافت نشد.");

            bool result = await _repository.DeleteAsync(product);

            if (!result) return OperationResult.Error("مشکل سمت سرور رخ داده است لطفا دقایقی دیگر تلاش کنید.");

            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
