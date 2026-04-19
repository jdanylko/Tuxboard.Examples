using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tuxboard.Core.Domain.Entities;

namespace AsyncWidgets.Data.Configuration;

public class DashboardDefaultWidgetConfiguration: IEntityTypeConfiguration<DashboardDefaultWidget>
{
    public void Configure(EntityTypeBuilder<DashboardDefaultWidget> builder)
    {
        builder.HasData(new List<DashboardDefaultWidget>
        {
            new()
            {
                DefaultWidgetId = Guid.NewGuid(),
                DashboardDefaultId = new Guid("1623F469-D9F0-400C-8A4C-B4366233F485"),
                LayoutRowId = new Guid("62487409-221B-40FF-A62B-FC3046B97CCB"),
                WidgetId = new Guid("EE84443B-7EE7-4754-BB3C-313CC0DA6039"),
                ColumnIndex = 1,
                WidgetIndex = 1
            }
        });
    }
}