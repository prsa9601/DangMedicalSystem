using Common.Domain;

namespace Domain.NotificationAgg.Events
{
    public class SendMessageEvent : BaseDomainEvent
    {
        public List<Guid> UserIds { get; set; }
    }
}
