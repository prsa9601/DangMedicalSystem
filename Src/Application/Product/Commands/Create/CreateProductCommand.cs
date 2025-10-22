using Common.Application;
using Common.Domain.ValueObjects;
using Domain.ProductAgg.Enum;
using Domain.ProductAgg.Interfaces.Repository;
using Domain.ProductAgg.Interfaces.Services;
using System.Globalization;

namespace Application.Product.Commands.Create
{
    public class CreateProductCommand : IBaseCommand
    {
        public required string slug { get; set; }
        public required string title{ get; set; }
        public required string description { get; set; }
        public required SeoData seoData { get; set; }
    }
    public sealed class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IProductDomainService _service;

        public CreateProductCommandHandler(IProductRepository repository, IProductDomainService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.ProductAgg.Product(request.title, 
                request.description, request.slug, request.seoData, _service);

            product.SetStatus(ProductStatus.NotActive);

            await _repository.AddAsync(product);
            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }
}
