using Common.Application;
using Common.Domain.ValueObjects;
using Domain.ProductAgg.Enum;
using Domain.ProductAgg.Interfaces.Repository;
using Domain.ProductAgg.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Application.Product.Commands.Create
{
    public class CreateProductCommand : IBaseCommand
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
                request.description, request.slug, new SeoData(
                    request.MetaKeyWords, request.MetaDescription, request.MetaTitle,
                    request.IndexPage, request.Canonical, request.Schema)
                , _service);

            product.SetStatus(ProductStatus.NotActive);

            await _repository.AddAsync(product);
            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }
}
