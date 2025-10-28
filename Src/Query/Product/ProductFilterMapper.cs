using Domain.ProductAgg;
using Query.Product.Dtos.FilterDto;
using Query.Product.DTOs;

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
    }
}
