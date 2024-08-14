using DefaultDashboards.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDashboards.Data.Configuration;

public class DashboardUserLoginConfiguration: IEntityTypeConfiguration<DashboardUserLogin>
{
    public void Configure(EntityTypeBuilder<DashboardUserLogin> builder)
    {
        builder.ToTable(nameof(DashboardUserLogin));
        builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });
    }
}