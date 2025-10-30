using Application.Utilities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.ValueObjects;
using Domain.ProductAgg.Enum;
using Domain.ProductAgg.Interfaces.Repository;
using Domain.ProductAgg.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace Application.Product.Commands.Create
{
    public class CreateProductCommand : IBaseCommand
    {
        public required string slug { get; set; }
        public required string title { get; set; }
        public required string description { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public IFormFile Image { get; set; }
        public ProductStatus status { get; set; }
        public string? MetaKeyWords { get; set; }
        public bool IndexPage { get; set; }
        public string? Canonical { get; set; }
        public string? Schema { get; set; }
    }
    public sealed class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IProductDomainService _service;
        private readonly IFileService _fileService;

        public CreateProductCommandHandler(IProductRepository repository, IProductDomainService service, IFileService fileService)
        {
            _repository = repository;
            _service = service;
            _fileService = fileService;
        }

        public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.ProductAgg.Product(request.title,
                request.description, request.slug, new SeoData(
                    request.MetaKeyWords, request.MetaDescription, request.MetaTitle,
                    request.IndexPage, request.Canonical, request.Schema)
                , _service);

            product.SetStatus(request.status);

            var imageName = await _fileService.SaveFileAndGenerateName
                (request.Image, Directories.ProductImagePath);
            product.SetImage(imageName);

            await _repository.AddAsync(product);
            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }
}
