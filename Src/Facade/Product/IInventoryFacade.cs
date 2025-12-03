using Application.Product.Commands.AddInventory;
using Application.Product.Commands.EditInventory;
using Application.Product.Commands.SetProfitableTime;
using Common.Application;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Query.SiteEntity.DTOs;

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
        private readonly IMemoryCache _cache;
        private readonly IMediator _mediator;

        public InventoryFacade(IMediator mediator, IMemoryCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<OperationResult> Create(AddInventoryCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditInventoryCommand command)
        {
            if (_cache.TryGetValue("MainPageContent", out MainPageDto? cachedResult))
            {
                _cache.Remove("MainPageContent");
            }
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> SetProfitableTime(SetInventoryProfitableCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
