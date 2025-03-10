using AsyncWidgets.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsyncWidgets.Data.Configuration;

public class TuxboardUserTokenConfiguration: IEntityTypeConfiguration<TuxboardUserToken>
{
    public void Configure(EntityTypeBuilder<TuxboardUserToken> builder)
    {
        builder.ToTable(nameof(TuxboardUserToken));

        builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
    }
}