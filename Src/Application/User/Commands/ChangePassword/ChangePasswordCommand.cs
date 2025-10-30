using Common.Application;
using Common.Application.SecurityUtil;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.User.Commands.ChangePassword
{
    public class ChangePasswordCommand :IBaseCommand
    {
        public Guid userId { get; set; }
        public required string password { get; set; }
    }
    internal class ChangePasswordCommandHandler : IBaseCommandHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository _repository;

        public ChangePasswordCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetTracking(request.userId);
            if (user == null) return OperationResult.NotFound();

            string password = Sha256Hasher.Hash(request.password);
            user.ChangePassword(password);

            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }
    }

}
