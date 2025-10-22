using Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.UserAgg
{
    public class UserOtp : BaseEntity
    {
        public Guid UserId { get; internal set; }
        public string OtpCode { get; private set; }
        public DateTime ExpireDate { get; private set; }

        public UserOtp(string otpCode, DateTime expireDate)
        {
            OtpCode = otpCode;
            ExpireDate = expireDate;
        }

    }
}
