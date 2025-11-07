using Domain.NotificationAgg.Interfaces.Repository;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.Ef.Notification.Repository
{
    internal class NotificationRepository : BaseRepository<Domain.NotificationAgg.Notification>, INotificationRepository
    {
        public NotificationRepository(Context context) : base(context)
        {
        }
    }
}
