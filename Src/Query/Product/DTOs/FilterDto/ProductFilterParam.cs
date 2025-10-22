using Common.Query.Filter;
using Domain.ProductAgg.Enum;

namespace Query.Product.DTOs.FilterDto
{
    public class ProductFilterParam : BaseFilterParam
    {
        public string? Title { get; set; }
        public ProductStatus? Status { get; set; }
    
    }
}
