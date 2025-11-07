using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.Order.Configuration
{
    internal class IOrderConfiguration : IEntityTypeConfiguration<Domain.OrderAgg.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.OrderAgg.Order> builder)
        {
            builder.ToTable("Orders", "order");
            builder.HasKey(b => b.Id);

            builder.OwnsMany(r => r.OrderItems, item =>
            {
                builder.ToTable("OrderItems", "order");
                builder.HasKey(b => b.Id);

                // اضافه کردن کانفیگوریشن‌های اضافی برای OrderItems
                item.Property(b => b.OrderId).IsRequired();
                item.Property(b => b.ProductId).IsRequired();
                item.Property(b => b.PricePerDong)
                    .IsRequired()
                    .HasConversion<string>(); // یا از نوع decimal استفاده کنید
                item.Property(b => b.DongAmount).IsRequired();
                item.Property(b => b.InventoryId).IsRequired();
            });
        }
    }
}
