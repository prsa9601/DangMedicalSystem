using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.Role.Configuration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Domain.RoleAgg.Role>
    {
        public void Configure(EntityTypeBuilder<Domain.RoleAgg.Role> builder)
        {
            builder.ToTable("Roles", "role");
            builder.HasKey(b => b.Id);

            // پیکربندی Propertyهای ساده
            builder.Property(b => b.Title).IsRequired(true).HasMaxLength(70);
        

            // پیکربندی ConcurrencyStamp



            // UserOtps
            builder.OwnsMany(u => u.RolePermissions, permission =>
            {
                permission.ToTable("RolePermissions", "role");
                permission.HasKey("Id");

                permission.HasIndex("RoleId");
            });
        }
    }
}
