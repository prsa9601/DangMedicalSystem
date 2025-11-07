using Common.Domain;

namespace Domain.OrderAgg.Interfaces.Event
{
    public class OrderEvent : BaseDomainEvent
    {
        public Guid OrderId { get; set; }

        public OrderEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
