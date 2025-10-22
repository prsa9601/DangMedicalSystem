using Common.Query;
using Domain.UserAgg.Enum;

namespace Query.User.DTOs
{
    public class UserAttemptDto : BaseDto
    {
        public Guid UserId { get; set; }
        public DateTime AttemptDate { get; set; }
        public bool IsSuccessful { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string FailureReason { get; set; }
        public DateTime ExpireDate { get; set; } // ✅ فیلد جدید
        public AttemptType AttemptType { get; set; }
    }
}
