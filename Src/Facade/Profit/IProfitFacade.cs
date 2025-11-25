using Application.Profit.Create;
using Common.Application;
using MediatR;

namespace Facade.Profit
{
    public interface IProfitFacade
    {
        Task<OperationResult> Create(CreateProfitCommand command);
    }
    public class ProfitFacade : IProfitFacade
    {
        private readonly IMediator _mediator;

        public ProfitFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(CreateProfitCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
