using Microsoft.EntityFrameworkCore;
using Tuxboard.Core.Data.Context;

namespace DefaultDashboards.Context;

public class RoleTuxboardContext : TuxDbContext, IRoleTuxboardContext
{
    public RoleTuxboardContext(DbContextOptions<TuxDbContext> options, IConfiguration config) 
        : base(options, config)
    {
    }

    public DbSet<RoleDashboard> RoleDashboard { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        var connString = _config.GetSection("TuxboardConfig")["ConnectionString"];
        if (!string.IsNullOrEmpty(connString))
        {
            optionsBuilder.UseSqlServer(connString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RoleDashboardConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}