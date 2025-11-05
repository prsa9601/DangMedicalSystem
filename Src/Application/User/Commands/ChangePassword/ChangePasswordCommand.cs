using Common.Application;
using Common.Application.SecurityUtil;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.User.Commands.ChangePassword
{
    public class ChangePasswordCommand :IBaseCommand
    {
        public Guid userId { get; set; }
        public string ipAddress { get; set; }
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

            if (CheckSessionForOtpCode(user, request.ipAddress) == false)
            {
                return OperationResult.Error("قبل از ثبت نام باید شماره تماستون رو فعال کنید.");
            }
            string password = Sha256Hasher.Hash(request.password);
            user.ChangePassword(password);

            await _repository.SaveChangeAsync();

            return OperationResult.Success();
        }

        private bool CheckSessionForOtpCode(Domain.UserAgg.User user, string ipAddress)
        {
            if (user == null)
                return false;

            var session = user.UserSessions.FirstOrDefault(i => i.ExpireDate > DateTime.Now && i.UserId.Equals(user.Id)
            && i.IpAddress == ipAddress && i.IsActive == true && ((i.ExpireDate.Minute - i.CreationDate.Minute) == 7));

            if (session is null)
                return false;

            session.ChangeActivity(false);
            return true;
        }
    }

}
