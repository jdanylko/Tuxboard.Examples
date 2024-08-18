using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tuxboard.Core.Domain.Entities;

namespace DefaultDashboards.Data.Configuration;

public class DashboardDefaultConfiguration: IEntityTypeConfiguration<DashboardDefault>
{
    public void Configure(EntityTypeBuilder<DashboardDefault> builder)
    {
        builder.HasData(new List<DashboardDefault>
        {
            new()
            {
                DefaultId = new Guid("1623F469-D9F0-400C-8A4C-B4366233F485"),
                LayoutId = new Guid("239C89ED-3310-40D8-9104-237659415392"),
                PlanId = null
            }
        });
    }
}