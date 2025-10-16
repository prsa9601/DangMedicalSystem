using Common.Domain;
using Common.Domain.ValueObjects;
using Domain.ProductAgg.Enum;

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

        public Product(string title, string description,
            string slug, SeoData seoData, ProductStatus status)
        {
            Title = title;
            Description = description;
            Slug = slug;
            SeoData = seoData;
            Status = status;
        }
        public void Edit(string title, string description,
            string slug, SeoData seoData, ProductStatus status)
        {
            Title = title;
            Description = description;
            Slug = slug;
            SeoData = seoData;
            Status = status;
        }
        public void SetImage(string imageName)
        {
            ImageName = imageName;
        }
        public void ChangeStatus(ProductStatus productStatus) 
        {
            Status = productStatus;
        }
    }
}
