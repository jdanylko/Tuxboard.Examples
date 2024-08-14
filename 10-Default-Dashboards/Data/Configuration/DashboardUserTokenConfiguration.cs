using DefaultDashboards.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDashboards.Data.Configuration;

public class DashboardUserTokenConfiguration: IEntityTypeConfiguration<DashboardUserToken>
{
    public void Configure(EntityTypeBuilder<DashboardUserToken> builder)
    {
        builder.ToTable("DashboardUserToken");

        builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
    }
}