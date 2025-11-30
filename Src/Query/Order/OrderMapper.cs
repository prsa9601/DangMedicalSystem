using Domain.OrderAgg;
using Microsoft.Identity.Client;
using Query.Order.DTOs;

namespace Query.Order
{
    public static class OrderMapper
    {
        public static OrderDto Map(this Domain.OrderAgg.Order order)
        {
            return new OrderDto
            {
                DateOfPurchase = order.DateOfPurchase,
                CreationDate = order.CreationDate,
                Id = order.Id,
                status = order.status,
                UserId = order.UserId,
                OrderItems = new OrderItemDto
                {
                    CreationDate = order.OrderItems.CreationDate,
                    Id = order.OrderItems.Id,
                    DongAmount = order.OrderItems.DongAmount,
                    InventoryId = order.OrderItems.InventoryId,
                    OrderId = order.OrderItems.OrderId,
                    PricePerDong = order.OrderItems.PricePerDong,
                    ProductId = order.OrderItems.ProductId,
                }
                //OrderItems = order.OrderItems.Map()
            };
        }

        public static List<OrderItemDto> Map(this List<Domain.OrderAgg.OrderItem> orderItems)
        {
            List<OrderItemDto> result = new();
            foreach (var item in orderItems)
            {
                result.Add(new OrderItemDto
                {
                    CreationDate = item.CreationDate,
                    Id = item.Id,
                    DongAmount = item.DongAmount,
                    InventoryId = item.InventoryId,
                    OrderId = item.OrderId,
                    PricePerDong = item.PricePerDong,
                    ProductId = item.ProductId,
                });
            }
            return result;
        }
    }
}
