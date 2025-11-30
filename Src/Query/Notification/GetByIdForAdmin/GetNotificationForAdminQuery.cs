using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Notification.DTOs;

namespace Query.Notification.GetByIdForAdmin
{
    public record class GetNotificationForAdminQuery(Guid id) : IQuery<NotificationDto?>;

    internal sealed class GetNotificationForAdminQueryHandler : IQueryHandler<GetNotificationForAdminQuery, NotificationDto?>
    {
        private readonly Context _context;

        public GetNotificationForAdminQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<NotificationDto?> Handle(GetNotificationForAdminQuery request, CancellationToken cancellationToken)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(i => i.Id.Equals(request.id));
            if (notification == null)
                return null;

            return notification.Map(_context);
        }
    }
}
