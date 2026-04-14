using CreatingWidgets.Data.Configuration;
using CreatingWidgets.Identity;
using CreatingWidgets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tuxboard.Core.Configuration;
using Tuxboard.Core.Data.Context;

namespace CreatingWidgets.Data.Context;

public class TuxboardRoleDbContext(DbContextOptions<TuxDbContext<Guid>> options, IOptions<TuxboardConfig> config)
    : TuxDbContext<Guid>(options, config), ITuxboardRoleDbContext
{
    public DbSet<RoleDefaultDashboard> RoleDefaultDashboards { get; set; }
    
    public DbSet<WidgetRole> WidgetRoles { get; set; }
    
    // Identity
    public DbSet<TuxboardUserClaim> TuxboardUserClaims { get; set; }
    public DbSet<TuxboardUserRole> TuxboardUserRoles { get; set; }
    public DbSet<TuxboardUserToken> TuxboardUserTokens { get; set; }
    public DbSet<TuxboardUserLogin> TuxboardUserLogins { get; set; }
    public DbSet<TuxboardUser> TuxboardUsers { get; set; }
    public DbSet<TuxboardRole> TuxboardRoles { get; set; }
    public DbSet<TuxboardRoleClaim> TuxboardRoleClaims { get; set; }

    public async Task<bool> DashboardExistsForAsync(Guid userId) =>
        await RoleDefaultDashboards.AnyAsync(rdd => rdd.Role.UserRoles.Any(Uri => Uri.UserId == userId));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleDefaultDashboardConfiguration());
        modelBuilder.ApplyConfiguration(new WidgetRoleConfiguration());

        modelBuilder.ApplyConfiguration(new DashboardLayoutConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardLayoutRowConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardDefaultConfiguration());
        modelBuilder.ApplyConfiguration(new DashboardDefaultWidgetConfiguration());

        // Identity
        modelBuilder.ApplyConfiguration(new TuxboardRoleConfiguration());
        modelBuilder.ApplyConfiguration(new TuxboardRoleClaimConfiguration());
        modelBuilder.ApplyConfiguration(new TuxboardUserConfiguration());
        modelBuilder.ApplyConfiguration(new TuxboardUserClaimConfiguration());
        modelBuilder.ApplyConfiguration(new TuxboardUserLoginConfiguration());
        modelBuilder.ApplyConfiguration(new TuxboardUserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new TuxboardUserTokenConfiguration());
    }
}