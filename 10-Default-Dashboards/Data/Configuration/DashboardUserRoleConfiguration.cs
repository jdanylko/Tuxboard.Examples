using DefaultDashboards.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDashboards.Data.Configuration;

public class DashboardUserRoleConfiguration: IEntityTypeConfiguration<DashboardUserRole>
{
    public void Configure(EntityTypeBuilder<DashboardUserRole> builder)
    {
        builder.ToTable("DashboardUserRole");

        builder.HasKey(r => new { r.UserId, r.RoleId });
    }
}