using DefaultDashboards.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDashboards.Data.Configuration;

public class DashboardUserConfiguration: IEntityTypeConfiguration<DashboardUser>
{
    public void Configure(EntityTypeBuilder<DashboardUser> builder)
    {
        builder.ToTable(nameof(DashboardUser));
    }
}