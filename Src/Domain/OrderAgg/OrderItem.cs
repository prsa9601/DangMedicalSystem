using Common.Domain;
using Domain.OrderAgg.Interfaces.Services;

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

        public void EditOrderItem(Guid productId, string pricePerDong, int dongAmount, Guid inventoryId)
        {
            ProductId = productId;
            PricePerDong = pricePerDong;
            DongAmount = dongAmount;
            InventoryId = inventoryId;
        }

        public async Task IncreaseDongAmount(int dongAmount, IOrderDomainService service)
        {
            int numberOfDongAvailable = await service.CheckNumberOfDongAvailable(ProductId);
            if (numberOfDongAvailable == 0)
                return;

            DongAmount = numberOfDongAvailable < dongAmount + DongAmount ? DongAmount = numberOfDongAvailable : DongAmount + dongAmount;

            DongAmount += dongAmount;
        }

        public void DecreaseDongAmount(int dongAmount, IOrderDomainService service)
        {
            DongAmount = DongAmount < dongAmount ? DongAmount = 0 : DongAmount - dongAmount;
        }

    }
}
