using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Order.GetById;
using Query.PurchaseReport.DTOs;

namespace Query.PurchaseReport.ReportUserInvestment
{
    public class ReportUserInvestmentQueryFilter : QueryFilter<PurchaseReportUserInvestmentFilterResult, UserPurchaseReportFilterParam>
    {
        public ReportUserInvestmentQueryFilter(UserPurchaseReportFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal sealed class ReportUserInvestmentQueryFilterQueryHandler :
        IQueryHandler<ReportUserInvestmentQueryFilter, PurchaseReportUserInvestmentFilterResult>
    {
        private readonly Context _context;

        public ReportUserInvestmentQueryFilterQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<PurchaseReportUserInvestmentFilterResult> Handle(ReportUserInvestmentQueryFilter request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = _context.PurchaseReports.OrderByDescending(p => p.CreationDate).AsQueryable();

            if (param.StartDate != DateTime.MinValue && param.StartDate != DateTime.MaxValue && param.StartDate is not null)
            {
                result = result.Where(purchase => purchase.CreationDate >= param.StartDate);
            }

            if (param.EndDate != DateTime.MinValue && param.EndDate != DateTime.MaxValue && param.EndDate is not null)
            {
                result = result.Where(purchase => purchase.CreationDate <= param.EndDate);
            }

            if (param.PurchaseReportFilter != null && param.PurchaseReportFilter == PurchaseReportFilter.None)
            {
                result = result.Where(purchase => purchase.CreationDate >= param.StartDate);
            }

            if (param.UserId != null && param.UserId != default)
            {
                result = result.Where(purchase => purchase.UserId.Equals(param.UserId));
            }

            if (param.PhoneNumber != null)
            {
                result = result.Where(purchase => purchase.UserId.Equals(
                    _context.Users.FirstOrDefault(
                        user => user.PhoneNumber.Equals(param.PhoneNumber))!.Id));
            }

            if (param.ProductId != null)
            {
                result = result.Where(purchase => purchase.ProductId.Equals(param.ProductId));
            }

            var skip = (@param.PageId - 1) * @param.Take;
            var model = new PurchaseReportUserInvestmentFilterResult()
            {
                Data = await (await result.Skip(skip).Take(@param.Take)
                    .Select(purchase => purchase).ToListAsync(cancellationToken)).MapUsersReport(_context),
                FilterParams = @param
            };

            model.GeneratePaging(result, @param.Take, @param.PageId);
            return model;
        }
    }
}
