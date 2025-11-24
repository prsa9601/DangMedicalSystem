namespace Domain.ProfitAgg.Service
{
    public interface IProfitService
    {
        bool CanCreate(Guid userId, Guid productId, Guid orderId);
    }
}
