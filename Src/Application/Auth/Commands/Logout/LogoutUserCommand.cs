using Common.Application;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Auth.Commands.Logout
{
    public class LogoutUserCommand : IBaseCommand
    {
        public Guid UserId { get; set; }
    }
    internal sealed class LogoutUserCommandHandler : IBaseCommandHandler<LogoutUserCommand>
    {
        private readonly IUserRepository _repository;

        public LogoutUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<OperationResult> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
