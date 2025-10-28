using Common.Application;
using Common.Domain.ValueObjects;
using Domain.ProductAgg.Interfaces.Repository;
using Domain.ProductAgg.Interfaces.Services;

namespace Application.Product.Commands.Edit
{
    public class EditProductCommand : IBaseCommand
    {
        public required string slug { get; set; }
        public required string title { get; set; }
        public required string description { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeyWords { get; set; }
        public bool IndexPage { get; set; }
        public string? Canonical { get; set; }
        public string? Schema { get; set; }
        public Guid productId { get; set; }
    }
    public sealed class EditProductCommandHandler : IBaseCommandHandler<EditProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IProductDomainService _service;

        public EditProductCommandHandler(IProductRepository repository, IProductDomainService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetTracking(request.productId);
            if (product == null) return OperationResult.NotFound("محصولی یافت نشد.");

            product.Edit(request.title, request.description, request.slug, new SeoData(
                    request.MetaKeyWords, request.MetaDescription, request.MetaTitle,
                    request.IndexPage, request.Canonical, request.Schema), _service);
            
            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }
}
