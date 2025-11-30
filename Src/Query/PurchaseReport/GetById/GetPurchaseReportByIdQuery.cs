using Common.Query;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Query.Product.GetById;
using Query.PurchaseReport.DTOs;

namespace Query.PurchaseReport.GetById
{
    public class GetPurchaseReportByIdQuery : IQuery<UserPurchaseReportDto?>
    {
        public Guid UserId { get; set; }
    }
    internal sealed class GetPurchaseReportByIdQueryHandler : IQueryHandler<GetPurchaseReportByIdQuery, UserPurchaseReportDto?>
    {
        private readonly Context _context;

        public GetPurchaseReportByIdQueryHandler(Context context)
        {
            _context = context;
        }

        public async Task<UserPurchaseReportDto?> Handle(GetPurchaseReportByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.PurchaseReports.Where(p => p.UserId.Equals(request.UserId)).ToListAsync();

            var model = await result.MapUserReport(_context) ?? null;
            return model;
        }
    }
}
