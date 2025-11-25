using Domain.StockAgg;
using Domain.StockAgg.Interfaces.Repository;
using Infrastructure;
using Infrastructure._Utilities;

namespace Infrastructure.Stock
{
    public class StockRepository : BaseRepository<Domain.StockAgg.Stock>, IStockRepository
    {
        public StockRepository(Context context) : base(context)
        {
        }
    }
}
