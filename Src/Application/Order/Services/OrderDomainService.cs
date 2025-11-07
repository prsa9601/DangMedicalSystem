using Domain.OrderAgg.Interfaces.Repository;
using Domain.OrderAgg.Interfaces.Services;

namespace Application.Order.Services
{
    internal class OrderDomainService : IOrderDomainService
    {
        private readonly IOrderRepository _repository;

        public OrderDomainService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CheckNumberOfDongAvailable(Guid productId)
        {
            var order = await _repository.GetListByFilterAsync(order =>
            order.OrderItems.Any(item =>
            item.ProductId.Equals(productId)) && order.status == Domain.OrderAgg.Enum.OrderStatus.paid);

            int dongCount = 0;
            dongCount += order.Sum(o => o.OrderItems.Sum(i => i.DongAmount));
            return 6 - dongCount;
        }
    }
}
