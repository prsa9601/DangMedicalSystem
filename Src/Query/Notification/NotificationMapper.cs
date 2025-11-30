using Domain.NotificationAgg;
using Domain.UserAgg;
using Infrastructure;
using Query.Notification.DTOs;
using System.Threading.Tasks;

namespace Query.Notification
{
    public static class NotificationMapper
    {
        public static NotificationDto Map(this Domain.NotificationAgg.Notification notification, Context context)
        {
            //var users = new List<Domain.UserAgg.User>();
            //foreach (var item in notification.UserId)
            //{
            //    var user = context.Users.FirstOrDefault(i=>i.Id.Equals(item));
            //    users.Add(user);
            //}
            var users = context.Users
    .Where(u => notification.UserId.Contains(u.Id))
    .ToList();

            return new NotificationDto
            {
                CreationDate = notification.CreationDate,
                Description = notification.Description,
                Id = notification.Id,
                Link = notification.Link ?? default,
                Title = notification.Title,
                Users = users.Select(i=>new UserNotificationDto
                {
                    Id = i.Id,
                    FirstName = i.FirstName,
                    CreationDate = i.CreationDate,
                    LastName = i.LastName,
                    PhoneNumber = i.PhoneNumber,
                    }).ToList(),
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
