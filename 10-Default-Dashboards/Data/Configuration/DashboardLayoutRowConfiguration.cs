using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tuxboard.Core.Domain.Entities;

namespace DefaultDashboards.Data.Configuration;

public class DashboardLayoutRowConfiguration: IEntityTypeConfiguration<LayoutRow>
{
    public void Configure(EntityTypeBuilder<LayoutRow> builder)
    {
        builder.HasData(new List<LayoutRow>
        {
            new()
            {
                LayoutRowId = new Guid("62487409-221B-40FF-A62B-FC3046B97CCB"),
                LayoutId = new Guid("239C89ED-3310-40D8-9104-237659415392"),
                LayoutTypeId= 4,
                RowIndex = 0
            }
        });
    }
}