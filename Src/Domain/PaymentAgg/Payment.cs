using Common.Domain;

namespace Domain.PaymentAgg
{
    public abstract class Payment : BaseEntity
    {
        public string TotalPrice { get; set; }
    }
}
