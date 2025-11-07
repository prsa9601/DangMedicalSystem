using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Order.DTOs;
using Query.Order.GetById;
using Query.PurchaseReport.DTOs;
using static Query.PurchaseReport.DTOs.PurchaseReportFilterParam;

namespace Query.PurchaseReport.ReportForAdmin
{
    public class GetPurchaseReportForAdmin : QueryFilter<PurchaseReportFilterResult, PurchaseReportFilterParam>
    {
        public GetPurchaseReportForAdmin(PurchaseReportFilterParam filterParams) : base(filterParams)
        {
        }
    }
    internal sealed class GetPurchaseReportForAdminQueryHandler : IQueryHandler<GetPurchaseReportForAdmin, PurchaseReportFilterResult>
    {
        private readonly Context _context;

        public GetPurchaseReportForAdminQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<PurchaseReportFilterResult> Handle(GetPurchaseReportForAdmin request, CancellationToken cancellationToken)
        {
            var @param = request.FilterParams;
            var result = _context.PurchaseReports.OrderByDescending(p => p.CreationDate).AsQueryable();

            if (param.StartDate != DateTime.MinValue && param.StartDate != DateTime.MaxValue)
            {
                result = result.Where(purchase => purchase.CreationDate >= param.StartDate);
            }

            if (param.EndDate != DateTime.MinValue && param.EndDate != DateTime.MaxValue)
            {
                result = result.Where(purchase => purchase.CreationDate <= param.EndDate);
            }

            if (param.PurchaseReportFilter != null && param.PurchaseReportFilter == PurchaseReportFilter.None)
            {
                result = result.Where(purchase => purchase.CreationDate >= param.StartDate);
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
            var model = new PurchaseReportFilterResult()
            {
                Data = await result.Skip(skip).Take(@param.Take)
                    .Select(purchase => purchase.Map()).ToListAsync(cancellationToken),
                FilterParams = @param
            };

            model.GeneratePaging(result, @param.Take, @param.PageId);
            return model;
        }
    }
}
