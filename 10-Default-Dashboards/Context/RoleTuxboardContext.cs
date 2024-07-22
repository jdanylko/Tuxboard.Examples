using Microsoft.EntityFrameworkCore;
using Tuxboard.Core.Data.Context;

namespace DefaultDashboards.Context;

public class RoleTuxboardContext(DbContextOptions<TuxDbContext> options, IConfiguration config)
    : TuxDbContext(options, config), IRoleTuxboardContext
{
    public DbSet<RoleDashboard> RoleDashboard { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RoleDashboardConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}