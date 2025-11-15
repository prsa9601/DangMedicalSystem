using Common.Domain.ValueObjects;
using Common.Query;
using Common.Query.Filter;
using Domain.ProductAgg.Enum;
using Query.Product.DTOs;
using Query.Product.DTOs.FilterDto;
using Query.PurchaseReport.DTOs;

namespace Query.Product.Dtos.FilterDto
{
    public class ProductFilterData : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string Slug { get; set; }
        public SeoData SeoData { get; set; }
        public ProductStatus Status { get; set; }

        public InventoryDto? InventoryDto { get; set; }
    }

    public class ProductFilterForIndexPageData : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string Slug { get; set; }
        public SeoData SeoData { get; set; }
        public ProductStatus Status { get; set; }

        public List<PurchaseReportDto>? PurchaseReportDto { get; set; }
        public InventoryDto? InventoryDto { get; set; }

    }


}
