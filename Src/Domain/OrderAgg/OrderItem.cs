using Common.Domain;
using System.Globalization;

namespace Domain.OrderAgg
{
    public class OrderItem : BaseEntity
    {
      

        public Guid OrderId { get; internal set; }
        public Guid ProductId { get; private set; }
        //قیمت هر دانگ
        public string PricePerDong { get; private set; }
        //مقدار خواسته شده
        public int DongAmount { get; private set; }
        public Guid InventoryId { get; private set; }

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
        
        public OrderItem(Guid productId, string pricePerDong, int dongAmount, Guid inventoryId)
        {
            ProductId = productId;
            PricePerDong = pricePerDong;
            DongAmount = dongAmount;
            InventoryId = inventoryId;
        }

        //increase And decrease
    }
}
