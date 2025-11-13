using Domain.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.User.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<Domain.UserAgg.User>
    {
        public void Configure(EntityTypeBuilder<Domain.UserAgg.User> builder)
        {

            builder.ToTable("Users", "user");
            builder.HasKey(b => b.Id);

            // پیکربندی Propertyهای ساده
            builder.Property(b => b.FirstName).IsRequired(false).HasMaxLength(70);
            builder.Property(b => b.LastName).IsRequired(false).HasMaxLength(70);
            builder.Property(b => b.PhoneNumber).IsRequired();
            builder.Property(b => b.Password).IsRequired();
            builder.Property(b => b.ImageName).IsRequired().HasDefaultValue("Default.png");
            builder.Property(b => b.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(b => b.CreationDate).IsRequired();

            // پیکربندی ConcurrencyStamp



            // UserOtps
            builder.OwnsMany(u => u.UserOtps, otp =>
            {
                otp.ToTable("UserOtps", "user");
                //otp.WithOwner().HasForeignKey("UserId");
                //otp.Property<Guid>("Id").ValueGeneratedOnAdd();
                otp.HasKey("Id");

                otp.Property(o => o.OtpCode).IsRequired().HasMaxLength(6);
                otp.Property(o => o.ExpireDate).IsRequired();
                otp.Property(o => o.CreationDate).IsRequired();

                otp.HasIndex("UserId");
            });

            //builder.HasMany(u => u.UserOtps)
            //    .WithOne()
            //    .HasForeignKey(o => o.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);


            // UserSessions
            builder.OwnsMany(u => u.UserSessions, session =>
            {
                session.ToTable("UserSessions", "user");
                //session.WithOwner().HasForeignKey("UserId");
                //session.Property<Guid>("Id").ValueGeneratedOnAdd();
                session.HasKey("Id");

                session.Property(s => s.JwtRefreshToken).IsRequired();
                session.Property(s => s.ExpireDate).IsRequired();
                session.Property(s => s.IpAddress).IsRequired();
                session.Property(s => s.IsActive).IsRequired().HasDefaultValue(true);
                session.Property(s => s.CreationDate).IsRequired();

                session.HasIndex("UserId");
            });

            // UserBlocks
            builder.OwnsMany(u => u.UserBlocks, block =>
            {
                block.ToTable("UserBlocks", "user");
                block.WithOwner().HasForeignKey("UserId");
                //block.Property<Guid>("Id").ValueGeneratedOnAdd();
                block.HasKey("Id");

                block.Property(b => b.BlockToDate).IsRequired();
                block.Property(b => b.Description).IsRequired().HasMaxLength(500);
                block.Property(b => b.IsActive).IsRequired().HasDefaultValue(true);
                block.Property(b => b.CreationDate).IsRequired();

                block.HasIndex("UserId");
            });

            // UserAttempts
            builder.OwnsMany(u => u.UserAttempts, attempt =>
            {
                attempt.ToTable("UserAttempts", "user");
                attempt.WithOwner().HasForeignKey("UserId");
                attempt.Property<Guid>("Id").ValueGeneratedOnAdd();
                attempt.HasKey("Id");

                attempt.Property(a => a.AttemptDate).IsRequired();
                attempt.Property(a => a.IsSuccessful).IsRequired();
                attempt.Property(a => a.IpAddress).IsRequired().HasMaxLength(45);
                attempt.Property(a => a.UserAgent).IsRequired(false).HasMaxLength(500);
                attempt.Property(a => a.FailureReason).IsRequired(false).HasMaxLength(500);
                attempt.Property(a => a.ExpireDate).IsRequired();
                attempt.Property(a => a.AttemptType).HasConversion<string>();
                attempt.Property(a => a.CreationDate).IsRequired();

                attempt.HasIndex("UserId");
            });

            // UserRole (Owned One-to-One)
            builder.OwnsOne(u => u.UserRole, role =>
            {
                role.ToTable("UserRoles", "user");
                role.WithOwner().HasForeignKey("UserId");
                role.Property<Guid>("Id").ValueGeneratedOnAdd();
                role.HasKey("Id");

                role.Property(r => r.RoleId).IsRequired();
                role.Property(r => r.CreationDate).IsRequired();

                role.HasIndex("UserId").IsUnique();
            });
          
            builder.OwnsOne(u => u.UserDocument, document =>
            {
                document.ToTable("UserDocument", "user");
                document.Property(b => b.NationalityCode).IsRequired(false);
                document.Property(b => b.NationalCardPhoto).IsRequired(false);
                document.Property(b => b.BirthCertificatePhoto).IsRequired(false);
                document.Property(b => b.Status).IsRequired().HasConversion<string>();

                document.HasIndex("UserId").IsUnique();
            });

            // BankAccount (Owned One-to-One)
            builder.OwnsOne(u => u.BankAccount, bankAccount =>
            {
                bankAccount.ToTable("BankAccounts", "user");
                bankAccount.WithOwner().HasForeignKey("UserId");
                bankAccount.Property<Guid>("Id").ValueGeneratedOnAdd();
                bankAccount.HasKey("Id");

                bankAccount.Property(ba => ba.Shaba).IsRequired().HasMaxLength(25);
                bankAccount.Property(ba => ba.CardNumber).IsRequired().HasMaxLength(16);
                bankAccount.Property(ba => ba.FirstName).IsRequired().HasMaxLength(70);
                bankAccount.Property(ba => ba.LastName).IsRequired().HasMaxLength(70);
                bankAccount.Property(ba => ba.IsConfirmed).IsRequired();
                bankAccount.Property(ba => ba.ExpirationDateMonth).IsRequired();
                bankAccount.Property(ba => ba.ExpirationDateYear).IsRequired();
                bankAccount.Property(ba => ba.CreationDate).IsRequired();
            });
            //builder.ToTable("User", "user");
            ////builder.HasKey(b => b.Id);

            //builder.Property(b => b.FirstName).IsRequired(false).HasMaxLength(70);
            //builder.Property(b => b.LastName).IsRequired(false).HasMaxLength(70);
            //builder.Property(b => b.BirthCertificatePhoto).IsRequired(false);
            //builder.Property(b => b.ImageName).IsRequired(true).HasDefaultValue("Default");
            //builder.Property(b => b.NationalCardPhoto).IsRequired(false);
            //builder.Property(b => b.NationalityCode).IsRequired(false);
            //builder.Property(b => b.CreationDate).IsRequired();

            //builder.Property(b => b.ConcurrencyStamp)
            //    .IsRowVersion()  // برای SQL Server
            //    .IsRequired()
            //    .IsConcurrencyToken().HasDefaultValueSql("NEWID()"); ;

            ////Otp Configuration
            //builder.OwnsMany(b => b.UserOtps, option =>
            //{
            //    option.ToTable("UserOtps", "user");
            //    option.HasKey(b => b.Id);
            //    option.HasIndex(b => b.UserId);
            //    //option.WithOwner(i => i.UserId);

            //    option.Property(b => b.OtpCode).IsRequired().HasMaxLength(6);
            //    option.Property(b => b.ExpireDate).IsRequired();
            //    option.Property(b => b.CreationDate).IsRequired();
            //    //option.Property<Guid>("UserId");
            //});


            //builder.OwnsMany(b => b.UserSessions, option =>
            //{
            //    option.ToTable("UserSessions", "user");
            //    option.HasKey(b => b.Id);

            //    option.Property(b => b.JwtRefreshToken).IsRequired();
            //    option.Property(b => b.ExpireDate).IsRequired();
            //    option.Property(b => b.IpAddress).IsRequired();

            //    option.HasIndex(b => b.UserId);
            //});

            //builder.OwnsOne(b => b.BankAccount, option =>
            //{
            //    option.ToTable("BankAccount", "user");
            //    option.HasKey(b => b.Id);

            //    option.Property(b => b.Shaba).IsRequired().HasMaxLength(25);
            //    option.Property(b => b.FirstName).IsRequired().HasMaxLength(70);
            //    option.Property(b => b.LastName).IsRequired().HasMaxLength(70);
            //    option.Property(b => b.CardNumber).IsRequired().HasMaxLength(16);

            //    option.HasIndex(b => b.UserId);
            //});
            //builder.OwnsOne(b => b.UserRole, option =>
            //{
            //    option.ToTable("UserRole", "user");
            //    option.HasKey(b => b.Id);

            //    option.HasIndex(b => b.UserId);
            //});

            //builder.OwnsMany(b => b.UserBlocks, option =>
            //{
            //    option.ToTable("UserBlocks", "user");
            //    option.HasKey(b => b.Id);

            //    option.Property(b => b.BlockToDate).IsRequired();
            //    option.Property(b => b.Description).IsRequired().HasMaxLength(500);
            //    option.Property(b => b.CreationDate).IsRequired();

            //    option.HasIndex(b => b.UserId);
            //});


            //builder.OwnsMany(b => b.UserAttempts, option =>
            //{
            //    option.ToTable("UserAttempts", "user");
            //    option.HasKey(b => b.Id);

            //    option.Property(b => b.AttemptDate).IsRequired();
            //    option.Property(b => b.IsSuccessful).IsRequired();
            //    option.Property(b => b.IpAddress).IsRequired().HasMaxLength(45);
            //    option.Property(b => b.UserAgent).IsRequired(false).HasMaxLength(500);
            //    option.Property(b => b.FailureReason).IsRequired(false).HasMaxLength(500);
            //    option.Property(b => b.ExpireDate).IsRequired();
            //    option.Property(b => b.AttemptType).HasConversion<string>();

            //    option.HasIndex(b => b.UserId);
            //});
        }
    }
}
