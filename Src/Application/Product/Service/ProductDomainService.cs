using Domain.ProductAgg.Interfaces.Repository;
using Domain.ProductAgg.Interfaces.Services;

namespace Application.Product.Service
{
    public class ProductDomainService : IProductDomainService
    {
        private readonly IProductRepository _repository;

        public ProductDomainService(IProductRepository repository)
        {
            _repository = repository;
        }

        public bool SlugIsExist(string slug)
        {
            return _repository.Exists(i=>i.Slug.Equals(slug));
        }
    }
}
