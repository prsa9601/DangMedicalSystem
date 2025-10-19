using AngleSharp.Dom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.Product.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Domain.ProductAgg.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.ProductAgg.Product> builder)
        {
            builder.ToTable("Product", "product");

            builder.HasKey(b => b.Id);

            builder.OwnsOne(u => u.SeoData, option =>
            {
                option.ToTable("SeoData", "product");

            });
           
            builder.Property(b => b.Slug)
               .IsRequired()
               .IsUnicode(false);

            builder.OwnsOne(b => b.SeoData, config =>
            {
                config.Property(b => b.MetaDescription)
                    .HasMaxLength(500)
                    .HasColumnName("MetaDescription");

                config.Property(b => b.MetaTitle)
                    .HasMaxLength(500)
                    .HasColumnName("MetaTitle");

                config.Property(b => b.MetaKeyWords)
                    .HasMaxLength(500)
                    .HasColumnName("MetaKeyWords");

                config.Property(b => b.IndexPage)
                    .HasColumnName("IndexPage");

                config.Property(b => b.Canonical)
                    .HasMaxLength(500)
                    .HasColumnName("Canonical");

                config.Property(b => b.Schema)
                    .HasColumnName("Schema");
            });
        }
    }
}
