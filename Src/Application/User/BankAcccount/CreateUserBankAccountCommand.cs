using Common.Application;

namespace Application.User.BankAcccount
{
    public class CreateUserBankAccountCommand : IBaseCommand
    {
        public Guid userId { get; set; }
        public string shabaNumber { get; set; }
        public string cardNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int ExpirationDateMonth { get; set; }
        public int ExpirationDateYear { get; set; }
    }
    internal sealed class CreateUserBankAccountCommandHandler : IBaseCommandHandler<CreateUserBankAccountCommand>
    {
        public Task<OperationResult> Handle(CreateUserBankAccountCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
