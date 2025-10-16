using Common.Domain;
using Domain.OrderAgg.Enum;

namespace Domain.OrderAgg
{
    public class Order : BaseEntity
    {
        public DateTime DateOfPurchase { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus status { get; set; }
        public List<OrderItem> OrderItems { get; set; }
      
        public Order(Guid userId)
        {
            UserId = userId;
        }

        public void SetOrderItem(Guid productId, string pricePerDong, int dongAmount, Guid inventoryId)
        {
            var orderItem = new OrderItem(productId, pricePerDong, dongAmount, inventoryId);
            orderItem.OrderId = Id;
            OrderItems.Add(orderItem);
        }

        public void IsPaid()
        {
            status = OrderStatus.paid;
        }
    }
}
