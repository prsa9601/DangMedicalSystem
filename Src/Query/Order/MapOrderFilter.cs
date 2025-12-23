using Query.Order.DTOs;

namespace Query.Order
{
    public static class MapOrderFilter
    {
        public static OrderFilterData MapFilterData(this Domain.OrderAgg.Order order)
        {
            if (order == null)
                return null;
            var orderDto = new OrderFilterData
            {
                DateOfPurchase = order.DateOfPurchase,
                CreationDate = order.CreationDate,
                Id = order.Id,
                status = order.status,
                UserId = order.UserId,
                //OrderItems = order.OrderItems.Map()
            };
            orderDto.OrderItems = order.OrderItems != null ? new OrderItemDto
            {
                DongAmount = order.OrderItems.DongAmount,
                PricePerDong = order.OrderItems.PricePerDong,
                CreationDate = order.CreationDate,
                Id = order.OrderItems.Id,
                InventoryId = order.OrderItems.InventoryId,
                OrderId = order.OrderItems.OrderId,
                ProductId = order.OrderItems.ProductId,
            } : new OrderItemDto { };
            
            return orderDto;
        }
    }
}
