using Application.Product.Commands.Create;
using Application.Product.Commands.Edit;
using Application.Product.Commands.Remove;
using Common.Application;
using MediatR;
using Query.Product.Dtos.FilterDto;
using Query.Product.DTOs;
using Query.Product.DTOs.FilterDto;
using Query.Product.GetById;
using Query.Product.GetBySlug;
using Query.Product.GetFilter;

namespace Facade.Product
{
    public interface IProductFacade
    {
        Task<OperationResult> RemoveProduct(RemoveProductCommand command);
        Task<OperationResult> CreateProduct(CreateProductCommand command);
        Task<OperationResult> EditProduct(EditProductCommand command);

        Task<ProductDto?> GetBySlug(string slug);
        Task<ProductDto?> GetById(Guid productId);
        Task<ProductFilterResult> GetByFilter(ProductFilterParam param);
    }
    public class ProductFacade : IProductFacade
    {
        private readonly IMediator _mediator;

        public ProductFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> CreateProduct(CreateProductCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> EditProduct(EditProductCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<ProductFilterResult> GetByFilter(ProductFilterParam param)
        {
            return await _mediator.Send(new GetProductFilterQuery(param));
        }

        public async Task<ProductDto?> GetById(Guid productId)
        {
            return await _mediator.Send(new GetProductByIdQuery(productId));
        }

        public async Task<ProductDto?> GetBySlug(string slug)
        {
            return await _mediator.Send(new GetProductBySlugQuery(slug));
        }

        public async Task<OperationResult> RemoveProduct(RemoveProductCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
