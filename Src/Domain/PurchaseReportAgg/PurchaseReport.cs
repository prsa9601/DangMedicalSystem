using Common.Domain;

namespace Domain.PurchaseReportAgg
{
    public class PurchaseReport : AggregateRoot
    {

        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
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


        public PurchaseReport(Guid userId, Guid orderId, Guid productId, string totalPrice,
            string totalDang, string totalProfit, string purchasePrice, int purchaseDang, 
            string profit, string profitPerDang, string purchasePricePerDang, int purchaseDangPerDang)
        {
            OrderId = orderId;
            UserId = userId;
            ProductId = productId;
            TotalPrice = totalPrice;
            TotalDang = totalDang;
            TotalProfit = totalProfit;
            PurchasePrice = purchasePrice;
            PurchaseDang = purchaseDang;
            Profit = profit;
            ProfitPerDang = profitPerDang;
            PurchasePricePerDang = purchasePricePerDang;
            PurchaseDangPerDang = purchaseDangPerDang;
        }
    }
}
