using DefaultDashboards.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDashboards.Data.Configuration;

public class DashboardUserClaimConfiguration : IEntityTypeConfiguration<DashboardUserClaim>
{
    public void Configure(EntityTypeBuilder<DashboardUserClaim> builder)
    {
        builder.ToTable(nameof(DashboardUserClaim));
    }
}