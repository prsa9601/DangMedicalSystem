using Common.Domain;

namespace Domain.PurchaseReport
{
    public class PurchaseReport : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        //اطلاعات کل خرید آن زمان
        public string TotalPrice { get; set; }
        public string TotalDang { get; set; }
        public string TotalProfit { get; set; }
        //اطلاعات خریداری شده
        public string PurchasePrice { get; set; }
        public int PurchaseDang { get; set; }
        public string Profit { get; set; }
        //اطلاعات اون موقع بر اساس یک دانگ
        public string ProfitPerDang { get; set; }
        public string PurchasePricePerDang { get; set; }
        public int PurchaseDangPerDang { get; set; }
    }
}
