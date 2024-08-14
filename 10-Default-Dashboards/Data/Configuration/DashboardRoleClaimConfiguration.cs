using DefaultDashboards.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDashboards.Data.Configuration;

public class DashboardRoleClaimConfiguration : IEntityTypeConfiguration<DashboardRoleClaim>
{
    public void Configure(EntityTypeBuilder<DashboardRoleClaim> builder)
    {
        builder.ToTable(nameof(DashboardRoleClaim));
    }
}