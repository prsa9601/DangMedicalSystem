using Application.Product.Commands.Create;
using Application.Product.Commands.Edit;
using Application.Product.Commands.Remove;
using Common.Application;
using MediatR;

namespace Facade.Product
{
    public interface IProductFacade
    {
        Task<OperationResult> RemoveProduct(RemoveProductCommand command);
        Task<OperationResult> CreateProduct(CreateProductCommand command);
        Task<OperationResult> EditProduct(EditProductCommand command);
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

        public async Task<OperationResult> RemoveProduct(RemoveProductCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
