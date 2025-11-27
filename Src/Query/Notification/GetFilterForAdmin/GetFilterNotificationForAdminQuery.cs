using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Notification.DTOs;
using Query.Order;
using Query.Order.DTOs;
using Query.Order.GetById;

namespace Query.Notification.GetFilterForAdmin
{
    public class GetFilterNotificationForAdminQuery : QueryFilter<NotificationFilterResult, NotificationFilterParam>
    {
        public GetFilterNotificationForAdminQuery(NotificationFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal sealed class GetFilterNotificationForAdminQueryHandler : IQueryHandler<GetFilterNotificationForAdminQuery, NotificationFilterResult>
    {
        private readonly Context _context;

        public GetFilterNotificationForAdminQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<NotificationFilterResult> Handle(GetFilterNotificationForAdminQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = _context.Notifications.OrderByDescending(i => i.CreationDate).AsQueryable();

           
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
            var model = new NotificationFilterResult()
            {
                Data = await result.Skip(skip).Take(@param.Take)
                    .Select(notification => notification.Map()).ToListAsync(cancellationToken),
                FilterParams = @param
            };

            model.GeneratePaging(result, @param.Take, @param.PageId);
            return model;
        }
    }
}
