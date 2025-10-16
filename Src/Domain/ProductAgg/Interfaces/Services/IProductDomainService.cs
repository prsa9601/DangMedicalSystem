namespace Domain.ProductAgg.Interfaces.Services
{
    public interface IProductDomainService
    {
        public Task<bool> SlugIsExist(string slug);
    }
}
