using Domain.OrderAgg.Interfaces.Repository;
using Domain.ProductAgg.Interfaces.Repository;
using Domain.ProfitAgg.Repository;
using Domain.ProfitAgg.Service;
using Domain.UserAgg.Interfaces.Repository;

namespace Application.Profit
{
    internal class ProfitService : IProfitService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProfitRepository _profitRepository;
        private readonly IOrderRepository _orderRepository;

        public ProfitService(IUserRepository userRepository, IProductRepository productRepository,
            IProfitRepository profitRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _profitRepository = profitRepository;
            _orderRepository = orderRepository;
        }

        public bool CanCreate(Guid userId, Guid productId, Guid orderId)
        {
            if (_userRepository.Exists(i => i.Id.Equals(userId)))
                return false;
            
            if (_productRepository.Exists(i => i.Id.Equals(productId)))
                return false;
            
            if (_orderRepository.Exists(i => i.Id.Equals(orderId)))
                return false;
            
            return true;
        }
    }
}
