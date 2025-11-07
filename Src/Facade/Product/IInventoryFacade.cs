using Application.Product.Commands.AddInventory;
using Application.Product.Commands.EditInventory;
using Application.Product.Commands.SetProfitableTime;
using Common.Application;
using MediatR;

namespace Facade.Product
{
    public interface IInventoryFacade
    {
        Task<OperationResult> Create(AddInventoryCommand command);
        Task<OperationResult> Edit(EditInventoryCommand command);
        Task<OperationResult> SetProfitableTime(SetInventoryProfitableCommand command);
    }
    public class InventoryFacade : IInventoryFacade
    {
        private readonly IMediator _mediator;

        public InventoryFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(AddInventoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditInventoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SetProfitableTime(SetInventoryProfitableCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
