namespace Domain.ProductAgg.Interfaces.Services
{
    public interface IProductDomainService
    {
        bool SlugIsExist(string slug);
    }
}
