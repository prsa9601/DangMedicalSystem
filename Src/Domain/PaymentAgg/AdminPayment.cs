namespace Domain.PaymentAgg
{
    public class AdminPayment : Payment
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public Guid PurchaseReportId { get; set; }
        public string ProfitPaymentPrice { get; set; }
    }
}
