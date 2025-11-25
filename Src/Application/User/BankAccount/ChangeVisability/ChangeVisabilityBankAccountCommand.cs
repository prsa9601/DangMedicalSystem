using Common.Application;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.User.BankAccount.ChangeVisability
{
    public class ChangeConfirmationBankAccountCommand : IBaseCommand
    {
        public Guid UserId { get; set; }
        public bool IsConfirmed { get; set; }
    }
    internal sealed class ChangeConfirmationBankAccountCommandHandler : IBaseCommandHandler<ChangeConfirmationBankAccountCommand>
    {
        private readonly IUserRepository _repository;

        public ChangeConfirmationBankAccountCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ChangeConfirmationBankAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTracking(request.UserId);
            if (user == null) return OperationResult.NotFound();

            user.ChangeConfirmationBankAccount(request.IsConfirmed);
            await _repository.SaveChangeAsync();
            return OperationResult.Success();
        }
    }
}
