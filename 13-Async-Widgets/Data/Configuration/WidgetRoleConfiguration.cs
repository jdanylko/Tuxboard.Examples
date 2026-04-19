using AsyncWidgets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsyncWidgets.Data.Configuration;

public class WidgetRoleConfiguration: IEntityTypeConfiguration<WidgetRole>
{
    public void Configure(EntityTypeBuilder<WidgetRole> builder)
    {
        builder.ToTable(nameof(WidgetRole));

        builder.HasKey(r => new { r.WidgetId, r.RoleId });
    }
}