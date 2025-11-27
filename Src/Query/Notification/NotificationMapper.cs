using Domain.NotificationAgg;
using Query.Notification.DTOs;

namespace Query.Notification
{
    public static class NotificationMapper
    {
        public static NotificationDto Map(this Domain.NotificationAgg.Notification notification)
        {
            return new NotificationDto
            {
                CreationDate = notification.CreationDate,
                Description = notification.Description,
                Id = notification.Id,
                Link = notification.Link ?? default,
                Title = notification.Title,
                UserIds = notification.UserId,
            };
        } 
        public static NotificationDtoForUser MapForUser(this Domain.NotificationAgg.Notification notification, Guid userId)
        {
            return new NotificationDtoForUser
            {
                CreationDate = notification.CreationDate,
                Description = notification.Description,
                Id = notification.Id,
                Link = notification.Link ?? default,
                Title = notification.Title,
                UserId = userId,
            };
        } 
    }
}
