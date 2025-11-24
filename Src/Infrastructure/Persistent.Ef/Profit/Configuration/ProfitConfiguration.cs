using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistent.Ef.Profit.Configuration
{
    internal class ProfitConfiguration : IEntityTypeConfiguration<Domain.ProfitAgg.Profit>
    {
        public void Configure(EntityTypeBuilder<Domain.ProfitAgg.Profit> builder)
        {
            builder.ToTable("Profits", "profit");
        }
    }
}
