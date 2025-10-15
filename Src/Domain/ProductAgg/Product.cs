using Common.Domain;

namespace Domain.ProductAgg
{
    public class Product : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public ProductStatus Status { get; set; }
    }
}
