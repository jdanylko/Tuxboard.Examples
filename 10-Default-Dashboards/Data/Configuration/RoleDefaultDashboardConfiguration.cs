using DefaultDashboards.Data.Context;
using DefaultDashboards.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDashboards.Data.Configuration;

public class RoleDefaultDashboardConfiguration: IEntityTypeConfiguration<RoleDefaultDashboard>
{
    public void Configure(EntityTypeBuilder<RoleDefaultDashboard> builder)
    {
        builder.HasKey(r => new { r.DefaultDashboardId, r.RoleId });

        builder.HasData(new List<RoleDefaultDashboard>
        {
            new()
            {
                RoleId = new Guid("7E69EB1F-07C0-46A1-B4E8-86F56386C250"), // Admin
                DefaultDashboardId = new Guid("0D96A18E-90B8-4A9F-9DF1-126653D68FE6") // Admin Dashboard
            },
            new()
            {
                RoleId = new Guid("31C3DF95-FDC6-4FB5-82AB-0436EA93C1B1"), // Basic
                DefaultDashboardId = new Guid("1623F469-D9F0-400C-8A4C-B4366233F485") // Basic dashboard
            }
        });
    }
}