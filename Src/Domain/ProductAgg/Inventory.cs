using Common.Domain;

namespace Domain.ProductAgg
{
    public class Inventory : BaseEntity
    {
        public Guid ProductId { get; internal set; }
        public string TotalPrice { get; private set; }
        public int Dong { get; private set; }
        public string Profit { get; private set; }
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

        public Inventory(string totalPrice)
        {
            TotalPrice = totalPrice;
        }

    }
}
