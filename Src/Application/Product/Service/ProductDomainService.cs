using Domain.OrderAgg.Interfaces.Repository;
using Domain.ProductAgg.Interfaces.Repository;
using Domain.ProductAgg.Interfaces.Services;

namespace Application.Product.Service
{
    public class ProductDomainService : IProductDomainService
    {
        private readonly IProductRepository _repository;
        private readonly IOrderRepository _orderRepository;

        public ProductDomainService(IProductRepository repository, IOrderRepository orderRepository)
        {
            _repository = repository;
            _orderRepository = orderRepository;
        }

        public bool CanDelete(Guid productId)
        {
            var order = _orderRepository.GetByFilterAsync
                (i => i.status == Domain.OrderAgg.Enum.OrderStatus.paid
                && i.OrderItems.ProductId.Equals(productId));

            return order == null;
        }

        public bool SlugIsExist(string slug)
        {
            return _repository.Exists(i => i.Slug.Equals(slug));
        }
    }
}
