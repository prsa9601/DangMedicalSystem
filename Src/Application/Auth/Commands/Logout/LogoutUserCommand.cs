using Common.Application;
using Common.Application.SecurityUtil;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Auth.Commands.Logout
{
    public class LogoutUserCommand : IBaseCommand
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; }
    }
    internal sealed class LogoutUserCommandHandler : IBaseCommandHandler<LogoutUserCommand>
    {
        private readonly IUserRepository _repository;

        public LogoutUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTracking(request.UserId);
            if (user is null)
                return OperationResult.NotFound();

            var userSession = user.UserSessions.FirstOrDefault(i => i.JwtRefreshToken.Equals(Sha256Hasher.Hash(request.RefreshToken)));

            user.RemoveSession(userSession);
            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }
}
