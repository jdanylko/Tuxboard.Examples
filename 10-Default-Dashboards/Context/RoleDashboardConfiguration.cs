using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultDashboards.Context;

public class RoleDashboardConfiguration : IEntityTypeConfiguration<RoleDashboard>
{
    public void Configure(EntityTypeBuilder<RoleDashboard> builder)
    {
        builder.ToTable(nameof(RoleDashboard));
        
        builder.HasKey(t => new { t.DashboardDefaultId, t.RoleId });

        builder
            .HasMany(d => d.VendorDocuments)
            .WithMany(p => p.VendorStatusHeaders)
            .UsingEntity<Dictionary<string, object>>(
                "VendorStatusDocument",
                r => r.HasOne<Document>().WithMany()
                    .HasForeignKey(nameof(Document.DocumentId))
                    .OnDelete(DeleteBehavior.ClientSetNull),
                l => l.HasOne<VendorStatusHeader>().WithMany()
                    .HasForeignKey(nameof(VendorStatusHeader.VendorStatusId))
                    .OnDelete(DeleteBehavior.ClientSetNull),
                j =>
                {
                    j.HasKey(nameof(VendorStatusHeader.VendorStatusId), nameof(Document.DocumentId));
                    j.ToTable("VendorStatusDocument");
                    j.IndexerProperty<int>(nameof(VendorStatusHeader.VendorStatusId)).HasColumnName("VendorStatusID");
                    j.IndexerProperty<int>(nameof(Document.DocumentId)).HasColumnName("DocumentID");
                });
    }
}