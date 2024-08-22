using DefaultWidgets.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultWidgets.Data.Configuration;

public class TuxboardUserClaimConfiguration : IEntityTypeConfiguration<TuxboardUserClaim>
{
    public void Configure(EntityTypeBuilder<TuxboardUserClaim> builder)
    {
        builder.ToTable(nameof(TuxboardUserClaim));
    }
}