using Common.Domain;

namespace Domain.UserAgg
{
    public class UserOtp : BaseEntity
    {
        public UserOtp(string otpCode, DateTime expireDate)
        {
            OtpCode = otpCode;
            ExpireDate = expireDate;
        }

        public Guid UserId { get; internal set; }
        public string OtpCode { get; private set; }
        public DateTime ExpireDate { get; private set; }
    }
}
