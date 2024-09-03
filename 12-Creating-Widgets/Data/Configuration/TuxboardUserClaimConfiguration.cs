using CreatingWidgets.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingWidgets.Data.Configuration;

public class TuxboardUserClaimConfiguration : IEntityTypeConfiguration<TuxboardUserClaim>
{
    public void Configure(EntityTypeBuilder<TuxboardUserClaim> builder)
    {
        builder.ToTable(nameof(TuxboardUserClaim));
    }
}