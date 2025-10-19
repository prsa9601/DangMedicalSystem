using Common.Domain;
using Domain.UserAgg.Enum;

namespace Domain.UserAgg
{
    public class UserAttempt : BaseEntity
    {
        public UserAttempt(DateTime attemptDate, bool isSuccessful,
            string ipAddress, string userAgent, string failureReason, 
            DateTime expireDate, AttemptType attemptType)
        {
            AttemptDate = attemptDate;
            IsSuccessful = isSuccessful;
            IpAddress = ipAddress;
            UserAgent = userAgent;
            FailureReason = failureReason;
            ExpireDate = expireDate;
            AttemptType = attemptType;
        }

        public Guid UserId { get; internal set; }
        public DateTime AttemptDate { get; private set; }
        public bool IsSuccessful { get; private set; }
        public string IpAddress { get; private set; }
        public string UserAgent { get; private set; }
        public string FailureReason { get; private set; }
        public DateTime ExpireDate { get; private set; } // ✅ فیلد جدید
        public AttemptType AttemptType { get; private set; }
    }
}
