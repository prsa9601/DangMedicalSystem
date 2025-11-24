using Domain.ProfitAgg.Repository;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.Ef.Profit.Repository
{
    public class ProfitRepository : BaseRepository<Domain.ProfitAgg.Profit>, IProfitRepository
    {
        public ProfitRepository(Context context) : base(context)
        {
        }
    }
}
