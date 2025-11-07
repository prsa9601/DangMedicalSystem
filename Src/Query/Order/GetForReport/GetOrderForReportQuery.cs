using Common.Query;
using Common.Query.Filter;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Order.DTOs;

namespace Query.Order.GetForReport
{
    public class GetOrderForReportQuery : QueryFilter<OrderFilterResult, OrderFilterParam>
    {
        public GetOrderForReportQuery(OrderFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal sealed class GetOrderForReportQueryHandler : IQueryHandler<GetOrderForReportQuery, OrderFilterResult>
    {
        private readonly Context _context;

        public GetOrderForReportQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<OrderFilterResult> Handle(GetOrderForReportQuery request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = _context.Orders.OrderByDescending(i => i.CreationDate).AsQueryable();

            if (param.StartDate != DateTime.MinValue && param.StartDate != DateTime.MaxValue)
            {
                result = result.Where(order => order.CreationDate >= param.StartDate);
            }

            if (param.EndDate != DateTime.MinValue && param.EndDate != DateTime.MaxValue)
            {
                result = result.Where(order => order.CreationDate <= param.EndDate);
            }

            if (param.OrderFilter != null && param.OrderFilter == OrderFilter.None)
            {
                result = result.Where(order => order.CreationDate >= param.StartDate);
            }

            if (param.PhoneNumber != null)
            {
                //var user = _context.Orders.FirstOrDefault();
                result = result.Where(order => order.UserId.Equals(
                    _context.Users.FirstOrDefault(
                        user => user.PhoneNumber.Equals(param.PhoneNumber))!.Id));
            }

            if (param.ProductId != null)
            {
                result = result.Where(order => order.OrderItems.Any(i =>
                i.ProductId.Equals(param.ProductId)));
            }

            var skip = (@param.PageId - 1) * @param.Take;
            var model = new OrderFilterResult()
            {
                Data = await result.Skip(skip).Take(@param.Take)
                    .Select(order => order.MapFilterData()).ToListAsync(cancellationToken),
                FilterParams = @param
            };

            model.GeneratePaging(result, @param.Take, @param.PageId);
            return model;
        }
    }
}