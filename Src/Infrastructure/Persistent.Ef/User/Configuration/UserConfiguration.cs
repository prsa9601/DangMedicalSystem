using Domain.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.User.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.UserAgg.User>
    {
        public void Configure(EntityTypeBuilder<Domain.UserAgg.User> builder)
        {
            builder.ToTable("User", "user");
            builder.HasKey(b => b.Id);

            builder.Property(b => b.FirstName).IsRequired().HasMaxLength(70);
            builder.Property(b => b.LastName).IsRequired().HasMaxLength(70);
            builder.Property(b => b.CreationDate).IsRequired();

            //Otp Configuration
            builder.OwnsMany(b => b.UserOtps, option =>
            {
                option.ToTable("UserOtps", "user");
                option.HasKey(b => b.Id);

                option.Property(b => b.OtpCode).IsRequired().HasMaxLength(6);
                option.Property(b => b.ExpireDate).IsRequired();
                option.Property(b => b.CreationDate).IsRequired();

                option.HasIndex(b => b.UserId);
            });

            builder.OwnsMany(b => b.UserSessions, option =>
            {
                option.ToTable("UserSessions", "user");
                option.HasKey(b => b.Id);

                option.Property(b => b.JwtRefreshToken).IsRequired();
                option.Property(b => b.ExpireDate).IsRequired();
                option.Property(b => b.IpAddress).IsRequired();

                option.HasIndex(b => b.UserId);
            });

            builder.OwnsOne(b => b.BankAccount, option =>
            {
                option.ToTable("BankAccount", "user");
                option.HasKey(b => b.Id);

                option.Property(b => b.Shaba).IsRequired().HasMaxLength(25);
                option.Property(b => b.FirstName).IsRequired().HasMaxLength(70);
                option.Property(b => b.LastName).IsRequired().HasMaxLength(70);
                option.Property(b => b.CardNumber).IsRequired().HasMaxLength(16);

                option.HasIndex(b => b.UserId);
            });

        }
    }
}
