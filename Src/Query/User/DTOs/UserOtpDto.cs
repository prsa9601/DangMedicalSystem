using Common.Query;

namespace Query.User.DTOs
{
    public class UserOtpDto : BaseDto
    {
        public Guid UserId { get; set; }
        public string OtpCode { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
