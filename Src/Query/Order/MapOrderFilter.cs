using Query.Order.DTOs;

namespace Query.Order
{
    public static class MapOrderFilter
    {
        public static OrderFilterData MapFilterData(this Domain.OrderAgg.Order order)
        {
            return new OrderFilterData
            {
                DateOfPurchase = order.DateOfPurchase,
                CreationDate = order.CreationDate,
                Id = order.Id,
                status = order.status,
                UserId = order.UserId,
                OrderItems = order.OrderItems.Map()
            };
        }
    }
}
