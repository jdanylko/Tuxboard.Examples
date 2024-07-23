using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DefaultDashboards.Identity;

public class DashboardIdentityContext(DbContextOptions<DashboardIdentityContext> options, IConfiguration config)
    : IdentityDbContext<DashboardIdentityUser, DashboardIdentityRole, Guid>(options)
{
    private readonly IConfiguration _config = config;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        var connString = _config.GetSection("ConnectionStrings")["DefaultConnection"];
        if (!string.IsNullOrEmpty(connString))
        {
            optionsBuilder.UseSqlServer(connString);
        }
    }
}