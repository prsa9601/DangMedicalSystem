using Common.Application;
using Domain.Contract;
using Domain.Contract.Repository;

namespace Application.Contract.Create
{
    public class CreateContractCommand : IBaseCommand
    {

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ContractStatus Status { get; set; }
    }
    internal sealed class CreateContractCommandHandler : IBaseCommandHandler<CreateContractCommand>
    {
        private readonly IContractRepository _repository;

        public CreateContractCommandHandler(IContractRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(CreateContractCommand request, CancellationToken cancellationToken)
        {
            var contract = new ContractAgg(request.PhoneNumber, request.Email, request.FullName, request.Title, request.Description);
            await _repository.AddAsync(contract);
            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }
}
