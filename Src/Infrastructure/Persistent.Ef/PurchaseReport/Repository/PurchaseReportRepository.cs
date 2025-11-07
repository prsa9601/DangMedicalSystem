using Domain.PurchaseReportAgg;
using Domain.PurchaseReportAgg.Interfaces.Repository;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.Ef.PurchaseReport.Repository
{
    internal class PurchaseReportRepository : BaseRepository<Domain.PurchaseReportAgg.PurchaseReport>, IPurchaseReportRepository
    {
        public PurchaseReportRepository(Context context) : base(context)
        {
        }
    }
}
