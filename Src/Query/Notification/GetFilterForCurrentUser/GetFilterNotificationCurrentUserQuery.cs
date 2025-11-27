using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Query.Notification.DTOs;

namespace Query.Notification.GetFilterForCurrentUser
{
    public class GetFilterNotificationCurrentUserQuery : QueryFilter<NotificationFilterResultForUser, NotificationFilterParamForUser>
    {
        public GetFilterNotificationCurrentUserQuery(NotificationFilterParamForUser filterParams) : base(filterParams)
        {
        }
    }
    internal sealed class GetFilterNotificationCurrentUserQueryHandler : IQueryHandler<GetFilterNotificationCurrentUserQuery, NotificationFilterResultForUser>
    {
        private readonly Context _context;

        public GetFilterNotificationCurrentUserQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<NotificationFilterResultForUser> Handle(GetFilterNotificationCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = _context.Notifications.OrderByDescending(i => i.CreationDate).AsQueryable();

            if (param.UserId == null || param.UserId == default)
            {
                return null;
            }

            if (param.Description != null)
            {
                //var user = _context.Orders.FirstOrDefault();
                result = result.Where(notification => notification.Title.Contains(param.Description));

            }

            if (param.Title != null)
            {
                result = result.Where(notification => notification.Title.Contains(param.Title));
            }

            var skip = (@param.PageId - 1) * @param.Take;
            var model = new NotificationFilterResultForUser()
            {
                Data = await result.Skip(skip).Take(@param.Take)
                    .Select(notification => notification.MapForUser(param.UserId)).ToListAsync(cancellationToken),
                FilterParams = @param
            };

            model.GeneratePaging(result, @param.Take, @param.PageId);
            return model;
        }
    }
}
