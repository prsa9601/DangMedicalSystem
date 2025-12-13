using Common.Application;
using Domain.Contract.Repository;

namespace Application.Contract.Answered
{
    public class ContractAnsweredCommand : IBaseCommand
    {
        public Guid id { get; set; }
    }
    internal sealed class ContractAnsweredCommandHandler : IBaseCommandHandler<ContractAnsweredCommand>
    {
        private readonly IContractRepository _repository;

        public ContractAnsweredCommandHandler(IContractRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ContractAnsweredCommand request, CancellationToken cancellationToken)
        {
            var contract = await _repository.GetTracking(request.id);
            if (contract == null) return OperationResult.NotFound();

            contract.IsAnswered();
            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }
}
