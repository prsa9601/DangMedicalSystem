using Common.Application;
using Common.Domain.BankInformationValidation.Strategy;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.User.BankAccount.Add
{
    public class AddBankAccountCommand : IBaseCommand
    {
        public required string CardNumber { get; set; }
        public required string ShabaNumber { get; set; }
        public required string FullName { get; set; }
        public required Guid UserId { get; set; }
    }

    public class AddBankAccountCommandHandler : IBaseCommandHandler<AddBankAccountCommand>
    {
        private readonly IUserRepository _repository;

        public AddBankAccountCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(AddBankAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTracking(request.UserId);
            if (user == null) return OperationResult.NotFound();

            user.AddBankAccount($"IR{request.ShabaNumber}", request.CardNumber.Replace(" ",""), request.FullName);

            try
            {
                await _repository.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return OperationResult.Error(ex.Message);
            }
            return OperationResult.Success();
        }
    }
}
