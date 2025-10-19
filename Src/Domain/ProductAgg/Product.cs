using Common.Domain;
using Common.Domain.Utils;
using Common.Domain.ValueObjects;
using Domain.ProductAgg.Enum;
using Domain.ProductAgg.Interfaces.Services;

namespace Domain.ProductAgg
{
    public class Product : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImageName { get; private set; }
        public string Slug { get; private set; }
        public SeoData SeoData { get; private set; }
        public ProductStatus Status { get; private set; }

        private Product()
        {
            SeoData = SeoData.CreateEmpty();
        }
        public Product(string title, string description,
            string slug, SeoData seoData, IProductDomainService service)
        {
            Guard(slug, title, description, service);
            Title = title;
            Description = description;
            Slug = slug.ToSlug();
            SeoData = seoData;
        }
        public void Edit(string title, string description,
            string slug, SeoData seoData, IProductDomainService service)
        {
            Guard(slug, title, description, service);
            Title = title;
            Description = description;
            Slug = slug.ToSlug();
            SeoData = seoData;
        }
        public void SetImage(string imageName)
        {
            ImageName = imageName;
        }
      
        public void SetStatus(ProductStatus status)
        {
            Status = status;
        }
       
        public void ChangeStatus(ProductStatus productStatus) 
        {
            Status = productStatus;
        }

        public void Guard(string slug, string title, string description, IProductDomainService service)
        {
            if (service.SlugIsExist(slug))
                throw new Exception("Slug Is Exist.");

            if (string.IsNullOrEmpty(title))
                throw new Exception("Title Can Not Be Null.");

            if (string.IsNullOrEmpty(description))
                throw new Exception("Description Can Not Be Null.");
        }
        public void SeoDataGuard(SeoData seoData)
        {
            if (string.IsNullOrEmpty(seoData.MetaTitle))
                throw new Exception("Meta Title Can Not Be Null");
        }
    }
}
