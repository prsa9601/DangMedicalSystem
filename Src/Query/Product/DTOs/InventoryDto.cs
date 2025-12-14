using Common.Query;
using Domain.ProductAgg.Enum;

namespace Query.Product.DTOs
{
    public class InventoryDto : BaseDto
    {
        public Guid ProductId { get; set; }
        public string TotalPrice { get; set; }
        public decimal Dong { get; set; }
        //سود هر دانگ
        public string Profit { get; set; }
        public PaymentTime ProfitableTime { get; set; }
        public string PricePerDong
        {
            get
            {
                if (decimal.TryParse(TotalPrice, out var total) && Dong > 0)
                {
                    var price = total / Dong;
                    return price.ToString("N0"); // فرمت عددی با جداکننده هزارگان
                }
                return "0";
            }
        }
    }
}
