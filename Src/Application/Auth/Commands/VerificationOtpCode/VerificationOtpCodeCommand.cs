using Common.Application;
using Common.Application.SecurityUtil;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Auth.Commands.VerificationOtpCode
{
    public class VerificationOtpCodeCommand : IBaseCommand<bool>
    {
        public string phoneNumber { get; set; }
        public string token { get; set; }
    }
    public class VerificationOtpCodeCommandHandler : IBaseCommandHandler<VerificationOtpCodeCommand, bool>
    {
        private readonly IUserRepository _repository;

        public VerificationOtpCodeCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<bool>> Handle(VerificationOtpCodeCommand request, CancellationToken cancellationToken)
        {
            string password = Sha256Hasher.Hash("DefaultGuestPassword");

            var user = await _repository.GetUserByFilter(user =>
                user.PhoneNumber.Equals(request.phoneNumber) 
                && user.Password != null );

            if (user is null)
                return OperationResult<bool>.NotFound(false, "اطفا دوباره درخواست ارسال کد فعال سازی بدهید.");

            var otp = user.UserOtps.OrderByDescending(o => o.CreationDate)
                .FirstOrDefault(otp => otp.ExpireDate > DateTime.Now && otp.UserId == user.Id);

            if (otp is null)
                return OperationResult<bool>.NotFound(false, "زمان کد فعال سازی شما به پایان رسیده لطفا دوباره درخواست دهید.");

            if (!otp.OtpCode.Equals(request.token))
                return OperationResult<bool>.Error("کد ارسالی شما اشتباه است");

            //Create UserSession

            return OperationResult<bool>.Success(true);
        }
    }
}
