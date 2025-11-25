using Application.Stock.Create;
using Common.Application;
using MediatR;

namespace Facade.Stock
{
    public interface IStockFacade
    {
        Task<OperationResult> Create(CreateStockCommand command);
    }
    public class StockFacade : IStockFacade
    {
        private readonly IMediator _mediator;

        public StockFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(CreateStockCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
