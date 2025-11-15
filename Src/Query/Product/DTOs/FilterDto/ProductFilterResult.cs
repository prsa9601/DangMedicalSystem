using Common.Query.Filter;
using Query.Product.Dtos.FilterDto;

namespace Query.Product.DTOs.FilterDto
{
    public class ProductFilterResult : BaseFilter<ProductFilterData, ProductFilterParam>
    {
    }

    public class ProductFilterForIndexPageResult : BaseFilter<ProductFilterForIndexPageData, ProductFilterParam>
    {
    }
}
