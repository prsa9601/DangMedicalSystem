namespace Domain.OrderAgg.Interfaces.Services
{
    public interface IOrderDomainService
    {
        Task<int> CheckNumberOfDongAvailable(Guid productId);
        Task<bool> CanPurchase(Guid productId);
    }
}
