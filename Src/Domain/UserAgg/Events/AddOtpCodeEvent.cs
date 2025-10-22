using Common.Domain;

namespace Domain.UserAgg.Events
{
    public class AddOtpCodeEvent : BaseDomainEvent
    {
        public Guid Id { get; set; }
        public string Session { get; set; }
    }
}
