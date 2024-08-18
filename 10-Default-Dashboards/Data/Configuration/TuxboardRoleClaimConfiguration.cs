using DefaultDashboards.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDashboards.Data.Configuration;

public class TuxboardRoleClaimConfiguration : IEntityTypeConfiguration<TuxboardRoleClaim>
{
    public void Configure(EntityTypeBuilder<TuxboardRoleClaim> builder)
    {
        builder.ToTable(nameof(TuxboardRoleClaim));
    }
}