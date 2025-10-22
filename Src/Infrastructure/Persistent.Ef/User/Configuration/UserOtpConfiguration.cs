//using Domain.UserAgg;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infrastructure.Persistent.Ef.User.Configuration
//{
//    public class UserOtpConfiguration : IEntityTypeConfiguration<UserOtp>
//    {
//        public void Configure(EntityTypeBuilder<UserOtp> builder)
//        {
//            builder.ToTable("UserOtps", "user");
//            builder.HasKey(o => o.Id);

//            builder.Property(o => o.OtpCode).IsRequired().HasMaxLength(6);
//            builder.Property(o => o.ExpireDate).IsRequired();
//            builder.Property(o => o.CreationDate).IsRequired();
//            builder.Property(o => o.UserId).IsRequired();

//            builder.HasIndex(o => o.UserId);
//        }
//    }
//}