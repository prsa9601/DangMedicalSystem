using Common.Domain;

namespace Domain.NotificationAgg
{
    public class Notification : BaseEntity
    {
        public Notification(string title, List<Guid>? userId, string description, string? link)
        {
            Title = title;
            UserId = userId;
            Description = description;
            Link = link;
        }
      
        public Notification(string title, Guid userId, string description, string? link)
        {
            Title = title;
            UserId.Add(userId);
            Description = description;
            Link = link;
        }

        public void Edit(string title, List<Guid> userId, string description, string? link)
        {
            Title = title;
            UserId = userId;
            Description = description;
            Link = link;
        }

        public string Title { get; set; }
        public List<Guid>? UserId { get; set; } = new List<Guid>();
        public string Description { get; set; }
        public string? Link { get; set; }
    }
}
