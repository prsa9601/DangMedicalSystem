using Common.Domain.ValueObjects;
using Common.Query;
using Domain.ProductAgg.Enum;

namespace Query.Product.DTOs
{
    public class ProductDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string Slug { get; set; }
        public SeoData SeoData { get; set; }
        public ProductStatus Status { get; set; }
    }
}
