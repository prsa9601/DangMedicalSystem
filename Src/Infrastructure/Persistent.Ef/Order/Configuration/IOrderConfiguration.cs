using Domain.OrderAgg.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.Order.Configuration
{
    internal class IOrderConfiguration : IEntityTypeConfiguration<Domain.OrderAgg.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.OrderAgg.Order> builder)
        {

            builder.ToTable("Orders","order");

            // Primary Key
            builder.HasKey(o => o.Id);

            // Properties configuration
            builder.Property(o => o.DateOfPurchase)
                   .IsRequired();

            builder.Property(o => o.UserId)
                   .IsRequired();

            builder.Property(o => o.status)
                   .IsRequired()
                   .HasConversion(
                       v => v.ToString(),
                       v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v)
                   )
                   .HasMaxLength(20);


            builder.OwnsMany(r => r.OrderItems, item =>
            {
                item.ToTable("OrderItems", "order");
                item.HasKey(b => b.Id);

                // اضافه کردن کانفیگوریشن‌های اضافی برای OrderItems
                item.Property(b => b.OrderId).IsRequired();
                item.Property(b => b.ProductId).IsRequired();
                item.Property(b => b.PricePerDong)
                    .IsRequired()
                    .HasConversion<string>(); // یا از نوع decimal استفاده کنید
                item.Property(b => b.DongAmount).IsRequired();
                item.Property(b => b.InventoryId).IsRequired();
            });

            // Indexes
            builder.HasIndex(o => o.UserId);
            builder.HasIndex(o => o.DateOfPurchase);
            builder.HasIndex(o => o.status);

            // Additional configurations (optional)
            builder.Property(o => o.CreationDate)
                   .HasDefaultValueSql("GETDATE()"); // If you have CreatedDate

            builder.Property(o => o.DateOfPurchase)
                   .HasDefaultValueSql("GETDATE()"); // If you have ModifiedDate
        }
    }
}
