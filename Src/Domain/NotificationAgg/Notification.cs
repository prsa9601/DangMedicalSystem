using Common.Domain;

namespace Domain.NotificationAgg
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
