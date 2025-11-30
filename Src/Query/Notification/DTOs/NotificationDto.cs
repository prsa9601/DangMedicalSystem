using Common.Query;
using Common.Query.Filter;
using Query.Product.Dtos.FilterDto;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace Query.Notification.DTOs
{
    public class NotificationDto : BaseDto
    {
        public string Title { get; set; }
        public List<UserNotificationDto> Users { get; set; } = new List<UserNotificationDto>();
        public string Description { get; set; }
        public string? Link { get; set; }
    }
    public class UserNotificationDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class NotificationDtoForUser : BaseDto
    {
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
    public class NotificationFilterParam : BaseFilterParam
    {
        [AllowNull]
        public string? Title { get; set; }
        [AllowNull]
        public string? Description { get; set; }
    }
    public class NotificationFilterParamForUser : BaseFilterParam
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public required Guid UserId { get; set; }
    }
    public class NotificationFilterResultForUser : BaseFilter<NotificationDtoForUser, NotificationFilterParamForUser>
    {

    }
    public class NotificationFilterResult : BaseFilter<NotificationDto, NotificationFilterParam>
    {

    }
}
