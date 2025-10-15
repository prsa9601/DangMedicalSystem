using Common.Domain;
using System.Globalization;

namespace Domain.OrderAgg
{
    public class OrderItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string PricePerDong { get; set; }
        //public int Dong { get; set; }
        //مقدار هر دانگ
        public int DongAmount { get; set; }
        public string PurchasedPrice { get; set; }

        public decimal TotalPrice
        {
            get
            {
                if (decimal.TryParse(PricePerDong, out var price))
                {
                    return DongAmount * price;
                }
                return 0;
            }
        }


    }
}
