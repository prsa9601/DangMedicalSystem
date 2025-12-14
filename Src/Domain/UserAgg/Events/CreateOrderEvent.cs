using Common.Domain;
using MediatR;

namespace Domain.UserAgg.Events
{
    public class CreateOrderEvent : BaseDomainEvent
    {
        public Guid UserId { get; set; }
    }
}
