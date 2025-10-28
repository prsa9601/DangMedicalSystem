using Query.Product.DTOs;

namespace Query.Product
{
    public static class ProductMapper
    {
        public static ProductDto Map(this Domain.ProductAgg.Product product)
        {
            return new ProductDto
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

        public static InventoryDto? MapInventory(this Domain.ProductAgg.Inventory? inventory)
        {
            if (inventory == null) return null;

            return new InventoryDto
            {
                Id = inventory.Id,
                CreationDate = inventory.CreationDate,
                Dong = inventory.Dong,
                ProductId = inventory.ProductId,
                Profit = inventory.Profit,
                ProfitableTime = inventory.ProfitableTime,
                TotalPrice = inventory.TotalPrice,
            };
        }

    }
}
