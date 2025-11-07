using Common.Domain;

namespace Domain.PaymentAgg
{
    public abstract class Payment : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public Guid PurchaseReportId { get; set; }
        public string ProfitPaymentPrice { get; set; }
    }
}
