using Common.Application;
using Domain.UserAgg.Enum;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.User.Commands.ConfirmedAccount
{
    public class ConfirmedAccountUserCommand : IBaseCommand
    {
        public Guid userId { get; set; }
        public UserStatus userStatus { get; set; }
    }
    internal class ConfirmedAccountUserCommandHandler : IBaseCommandHandler<ConfirmedAccountUserCommand>
    {
        private readonly IUserRepository _repository;

        public ConfirmedAccountUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ConfirmedAccountUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTracking(request.userId);
            if (user == null) OperationResult.NotFound("کاربری یافت نشد.");

            user.SetUserStatus(request.userStatus);
            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }
}
