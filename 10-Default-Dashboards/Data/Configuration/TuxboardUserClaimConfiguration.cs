using DefaultDashboards.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDashboards.Data.Configuration;

public class TuxboardUserClaimConfiguration : IEntityTypeConfiguration<TuxboardUserClaim>
{
    public void Configure(EntityTypeBuilder<TuxboardUserClaim> builder)
    {
        builder.ToTable(nameof(TuxboardUserClaim));
    }
}