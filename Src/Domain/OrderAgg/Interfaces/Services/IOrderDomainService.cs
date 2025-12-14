namespace Domain.OrderAgg.Interfaces.Services
{
    public interface IOrderDomainService
    {
        Task<decimal> CheckNumberOfDongAvailable(Guid productId);
        Task<bool> CanPurchase(Guid productId);
    }
}
