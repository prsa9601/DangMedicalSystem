using Domain.ProductAgg.Interfaces.Repository;
using Infrastructure._Utilities;

namespace Infrastructure.Persistent.Ef.Product.Repository
{
    public class ProductRepository : BaseRepository<Domain.ProductAgg.Product>, IProductRepository
    {
        public ProductRepository(Context context) : base(context)
        {
        }
    }
}
