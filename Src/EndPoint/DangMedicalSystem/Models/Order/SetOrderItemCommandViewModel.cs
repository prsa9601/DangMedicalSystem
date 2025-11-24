namespace DangMedicalSystem.Api.Models.Order
{
    public class SetOrderItemCommandViewModel
    {
        public Guid orderId { get; set; }
        public Guid productId { get; set; }
        public Guid inventoryId { get; set; }
        public int dongAmount { get; set; }
    }
    public class OrderIsFinallyViewModel
    {
        public Guid orderId { get; set; }
    }
}
