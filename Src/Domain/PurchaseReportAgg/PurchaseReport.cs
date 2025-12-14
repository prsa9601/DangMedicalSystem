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
        public decimal PurchaseDang { get; set; }
        public string Profit { get; set; }
        //اطلاعات اون موقع بر اساس یک دانگ
        public string ProfitPerDang { get; set; }
        public string PurchasePricePerDang { get; set; }
        public decimal PurchaseDangPerDang { get; set; }


        public PurchaseReport(Guid userId, Guid orderId, Guid productId, string totalPrice,
            string totalDang, string totalProfit, string purchasePrice, decimal purchaseDang, 
            string profit, string profitPerDang, string purchasePricePerDang, decimal purchaseDangPerDang)
        {
            Common.Domain.DomainValidation.DecimalValidation.DecimalGuard(purchasePrice);
            Common.Domain.DomainValidation.DecimalValidation.DecimalGuard(totalProfit);
            Common.Domain.DomainValidation.DecimalValidation.DecimalGuard(totalPrice);
            Common.Domain.DomainValidation.DecimalValidation.DecimalGuard(profit);
            Common.Domain.DomainValidation.DecimalValidation.DecimalGuard(profitPerDang);
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
