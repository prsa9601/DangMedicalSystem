using Common.Domain;
using Domain.ProductAgg.Enum;

namespace Domain.ProductAgg
{
    public class Inventory : BaseEntity
    {
        public Guid ProductId { get; internal set; }
        public string TotalPrice { get; private set; }
        public int Dong { get; private set; }
        //سود هر دانگ
        public string Profit { get; private set; }
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
            //private set;
        }

        public Inventory(string totalPrice, int dong, string profit)
        {
            TotalPrice = totalPrice;
            Dong = dong;
            Profit = profit;
        }

        public void EditInventory(string totalPrice, int dong, string profit)
        {
            TotalPrice = totalPrice;
            Dong = dong;
            Profit = profit;
        }

        public void SetProfitableTime(PaymentTime paymentTime)
        {
            ProfitableTime = paymentTime;
        }
    }
}
