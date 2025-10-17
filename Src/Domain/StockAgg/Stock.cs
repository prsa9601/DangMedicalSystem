using Common.Domain;

namespace Domain.StockAgg
{
    // احتمالا باید حذف بشه
    public class Stock : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        //public Guid PurchaseId { get; set; }
        public int Dang { get; set; }
        public string TotalPrice { get; set; }
        public DateTime NextPaymentDate { get; set; }
    }
}