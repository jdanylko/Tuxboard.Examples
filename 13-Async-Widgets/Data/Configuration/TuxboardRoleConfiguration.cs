using AsyncWidgets.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsyncWidgets.Data.Configuration;

public class TuxboardRoleConfiguration: IEntityTypeConfiguration<TuxboardRole>
{
    public void Configure(EntityTypeBuilder<TuxboardRole> builder)
    {
        builder.ToTable(nameof(TuxboardRole));
        
        builder.HasData(new List<TuxboardRole>
        {
            new()
            {
                Id = new Guid("31C3DF95-FDC6-4FB5-82AB-0436EA93C1B1"),
                Name = "Basic",
                NormalizedName = "BASIC",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new()
            {
                Id = new Guid("7E69EB1F-07C0-46A1-B4E8-86F56386C250"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        });
    }
}