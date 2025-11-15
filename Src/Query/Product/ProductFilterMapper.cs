using Domain.ProductAgg;
using Infrastructure;
using Query.Product.Dtos.FilterDto;
using Query.Product.DTOs;
using Query.PurchaseReport;

namespace Query.Product
{
    public static class ProductFilterMapper
    {
        public static ProductFilterData MapProductFilterData(this Domain.ProductAgg.Product product)
        {
            return new ProductFilterData
            {
                Id = product.Id,
                CreationDate = product.CreationDate,
                Description = product.Description,
                ImageName = product.ImageName,
                InventoryDto = product.Inventory.MapInventory() ?? null,
                SeoData = product.SeoData,
                Slug = product.Slug,
                Status = product.Status,
                Title = product.Title,
            };
        }
        public static ProductFilterForIndexPageData MapProductFilterForIndexPageData(
            this Domain.ProductAgg.Product product, Context context)
        {
            return new ProductFilterForIndexPageData
            {
                Id = product.Id,
                CreationDate = product.CreationDate,
                Description = product.Description,
                ImageName = product.ImageName,
                InventoryDto = product.Inventory.MapInventory() ?? null,
                SeoData = product.SeoData,
                Slug = product.Slug,
                Status = product.Status,
                Title = product.Title,
                PurchaseReportDto = context.PurchaseReports.Where(i => i.ProductId.Equals(product.Id))
                .Select(i => i.Map()).ToList(),
            };
        }
    }
}
