using Domain.OrderAgg.Interfaces.Repository;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.Ef.Order.Repository
{
    internal class OrderRepository : BaseRepository<Domain.OrderAgg.Order>, IOrderRepository
    {
        public OrderRepository(Context context) : base(context)
        {
        }
    }
}
