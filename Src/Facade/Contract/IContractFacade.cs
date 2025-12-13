using Application.Contract.Answered;
using Application.Contract.Create;
using Common.Application;
using MediatR;
using Query.Contract.DTOs;
using Query.Contract.GetFilter;

namespace Facade.Contract
{
    public interface IContractFacade
    {
        Task<OperationResult> CreateContract(CreateContractCommand command);
        Task<OperationResult> AnsweredContract(ContractAnsweredCommand command);
        Task<ContractFilterResult> GetFilter(ContractFilterParam filterParam);
    }
    internal class ContractFacade : IContractFacade
    {
        private readonly IMediator _mediator;

        public async Task<OperationResult> AnsweredContract(ContractAnsweredCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> CreateContract(CreateContractCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<ContractFilterResult> GetFilter(ContractFilterParam filterParam)
        {
            return await _mediator.Send(new GetContractByFilter(filterParam));
        }
    }
}
