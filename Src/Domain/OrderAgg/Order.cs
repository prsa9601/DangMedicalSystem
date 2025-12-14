using Common.Domain;
using Domain.OrderAgg.Enum;
using Domain.OrderAgg.Interfaces.Services;
using System.Net.Http.Headers;
using System.Net.Sockets;

namespace Domain.OrderAgg
{
    public class Order : AggregateRoot
    {
        public DateTime DateOfPurchase { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus status { get; set; }
        public OrderItem? OrderItems { get; set; }
        //public List<OrderItem> OrderItems { get; set; }

        public Order(Guid userId)
        {
            DateOfPurchase = DateTime.MinValue;
            status = OrderStatus.AwaitingPayment;
            UserId = userId;
        }

        public void SetOrderItem(Guid productId, string pricePerDong, decimal dongAmount, Guid inventoryId)
        {
            var orderItem = new OrderItem(productId, pricePerDong, dongAmount, inventoryId);
            orderItem.OrderId = Id;
            OrderItems = orderItem;
        }

        public void IsPaid()
        {
            status = OrderStatus.paid;
        }
        //public void RemoveOrderItem(List<OrderItem> orderItems)
        //{
        //    foreach (var item in orderItems)
        //    {
        //        OrderItems.Remove(item);
        //    }
        //}
        //public void AddOrderItem(List<OrderItem> orderItems)
        //{
        //    OrderItems.AddRange(orderItems);
        //}

        //public void AddOrderItem(Guid productId, string pricePerDong, int dongAmount, Guid inventoryId)
        //{
        //    var orderItem = new OrderItem(productId, pricePerDong, dongAmount, inventoryId);
        //    OrderItems.Add(orderItem);
        //}


        //public void EditOrderItem(Guid itemId, Guid productId, string pricePerDong, int dongAmount, Guid inventoryId)
        //{
        //    var order = OrderItems.FirstOrDefault(x => x.ProductId == productId && x.OrderId.Equals(Id) && x.Id.Equals(itemId));
        //    order.EditOrderItem(productId, pricePerDong, dongAmount, inventoryId);
        //}

        //public void RemoveOrderItem(Guid itemId, Guid productId, string pricePerDong, int dongAmount, Guid inventoryId)
        //{
        //    var order = OrderItems.FirstOrDefault(x => x.ProductId == productId && x.OrderId.Equals(Id) && x.Id.Equals(itemId));
        //    OrderItems.Remove(order);
        //}

        //public void IncreaseItem(Guid itemId, Guid productId, string pricePerDong, int dongAmount, Guid inventoryId, IOrderDomainService service)
        //{
        //    var order = OrderItems.FirstOrDefault(x => x.ProductId == productId && x.OrderId.Equals(Id) && x.Id.Equals(itemId));
        //    order.IncreaseDongAmount(dongAmount, service);
        //}

        //public void DecreaseItem(Guid itemId, Guid productId, string pricePerDong, int dongAmount, Guid inventoryId, IOrderDomainService service)
        //{
        //    var order = OrderItems.FirstOrDefault(x => x.ProductId == productId && x.OrderId.Equals(Id) && x.Id.Equals(itemId));
        //    order.DecreaseDongAmount(dongAmount, service);
        //}
    }
}
