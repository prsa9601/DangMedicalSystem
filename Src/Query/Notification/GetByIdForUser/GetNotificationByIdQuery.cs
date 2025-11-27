using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Notification.DTOs;

namespace Query.Notification.GetByIdForUser
{
    public record class GetNotificationByIdQuery(Guid id, Guid userId) : IQuery<NotificationDtoForUser?>;

    internal sealed class GetNotificationByIdQueryHandler : IQueryHandler<GetNotificationByIdQuery, NotificationDtoForUser>
    {
        private readonly Context _context;

        public GetNotificationByIdQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<NotificationDtoForUser> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(i => i.Id.Equals(request.id));
            if (notification == null)
                return null;

            return notification.MapForUser(request.userId);
        }
    }
}
