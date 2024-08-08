using DefaultDashboards.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using Tuxboard.Core.Configuration;
using Tuxboard.Core.Data.Context;
using Tuxboard.Core.Domain.Entities;

namespace DefaultDashboards.Data.Context;

public interface ITuxboardRoleDbContext: ITuxDbContext
{
    DbSet<RoleDefaultDashboard> RoleDefaultDashboards { get; set; }
}

public class TuxboardRoleDbContext : TuxDbContext, ITuxboardRoleDbContext
{
    public TuxboardRoleDbContext(DbContextOptions<TuxDbContext> options, IOptions<TuxboardConfig> config)
        : base(options, config)
    {
    }

    public DbSet<RoleDefaultDashboard> RoleDefaultDashboards { get; set; }
    
    // REF: https://github.com/dotnet/aspnetcore/issues/5793
    public DbSet<IdentityUserClaim<Guid>> DashboardUserClaims { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // REF: https://github.com/dotnet/aspnetcore/issues/5793
        modelBuilder.Entity<IdentityUserClaim<Guid>>().HasKey(p => new { p.Id });
        
        modelBuilder.ApplyConfiguration(new RoleDefaultDashboardConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardUserLoginConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardUserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardUserTokenConfiguration());
    }
}

public class DashboardUserTokenConfiguration: IEntityTypeConfiguration<DashboardUserToken>
{
    public void Configure(EntityTypeBuilder<DashboardUserToken> builder)
    {
        builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
    }
}

public class DashboardUserRoleConfiguration: IEntityTypeConfiguration<DashboardUserRole>
{
    public void Configure(EntityTypeBuilder<DashboardUserRole> builder)
    {
        builder.HasKey(r => new { r.UserId, r.RoleId });
    }
}

public class DashboardUserLoginConfiguration: IEntityTypeConfiguration<DashboardUserLogin>
{
    public void Configure(EntityTypeBuilder<DashboardUserLogin> builder)
    {
        builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });
    }
}

public class RoleDefaultDashboardConfiguration: IEntityTypeConfiguration<RoleDefaultDashboard>
{
    public void Configure(EntityTypeBuilder<RoleDefaultDashboard> builder)
    {
        builder.HasKey(r => new { r.DefaultDashboardId, r.RoleId });
    }
}

public class RoleDefaultDashboard
{
    public virtual Guid DefaultDashboardId { get; set; }
    public virtual Guid RoleId { get; set; }

    public virtual DashboardDefault DefaultDashboard  { get; set; } = default!;
    public virtual DashboardRole Role { get; set; } = default!;
}

