using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tuxboard.Core.Domain.Entities;

namespace DefaultDashboards.Data.Configuration;

public class DashboardLayoutConfiguration: IEntityTypeConfiguration<Layout>
{
    public void Configure(EntityTypeBuilder<Layout> builder)
    {
        builder.HasData(new List<Layout>
        {
            new()
            {
                LayoutId = new Guid("239C89ED-3310-40D8-9104-237659415392"),
                TabId = null,
                LayoutIndex = 1
            }
        });
    }
}