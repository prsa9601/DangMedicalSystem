using Common.Domain;
using Domain.ProfitAgg;

namespace Domain.StockAgg
{
    // احتمالا باید حذف بشه و در یوزر پیاده سازی بشه
    public class Stock : BaseEntity
    {
        public Stock(Guid productId, Guid userId, Guid purchaseId, string amountPaid, DateTime nextPaymentDate, int paymentNumber)
        {
            Common.Domain.DomainValidation.DecimalValidation.DecimalGuard(amountPaid);
            ProductId = productId;
            UserId = userId;
            PurchaseId = purchaseId;
            AmountPaid = amountPaid;
            NextPaymentDate = nextPaymentDate;
            PaymentNumber = paymentNumber;
        }

        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public Guid PurchaseId { get; set; }
        public string AmountPaid { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public int PaymentNumber { get; set; }
    }
}