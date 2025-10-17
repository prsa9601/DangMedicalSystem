namespace Domain.ProductAgg.Interfaces.Services
{
    public interface IProductDomainService
    {
        bool SlugIsExist(string slug);
        bool PhoneNumberIsExist(string phoneNumber);
    }
}
