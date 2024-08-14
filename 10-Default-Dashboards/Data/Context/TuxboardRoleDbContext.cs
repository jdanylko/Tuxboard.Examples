using DefaultDashboards.Data.Configuration;
using DefaultDashboards.Identity;
using DefaultDashboards.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tuxboard.Core.Configuration;
using Tuxboard.Core.Data.Context;

namespace DefaultDashboards.Data.Context;

public class TuxboardRoleDbContext : TuxDbContext, ITuxboardRoleDbContext
{
    public TuxboardRoleDbContext(DbContextOptions<TuxDbContext> options, IOptions<TuxboardConfig> config)
        : base(options, config)
    {
    }

    public DbSet<RoleDefaultDashboard> RoleDefaultDashboards { get; set; }
    
    // Identity
    public DbSet<DashboardUserClaim> DashboardUserClaims { get; set; }
    public DbSet<DashboardUserRole> DashboardUserRoles { get; set; }
    public DbSet<DashboardUserToken> DashboardUserTokens { get; set; }
    public DbSet<DashboardUserLogin> DashboardUserLogins { get; set; }
    public DbSet<DashboardUser> DashboardUsers { get; set; }
    public DbSet<DashboardRole> DashboardRoles { get; set; }
    public DbSet<DashboardRoleClaim> DashboardRoleClaims { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleDefaultDashboardConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardLayoutConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardLayoutRowConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardDefaultConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardDefaultWidgetConfiguration());

        // Identity
        modelBuilder.ApplyConfiguration(new DashboardRoleConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardRoleClaimConfiguration());

        modelBuilder.ApplyConfiguration(new DashboardUserConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardUserClaimConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardUserLoginConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardUserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardUserTokenConfiguration());
    }
}