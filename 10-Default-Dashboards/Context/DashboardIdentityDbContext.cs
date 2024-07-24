using DefaultDashboards.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DefaultDashboards.Context;

public class DashboardIdentityDbContext : IdentityDbContext<DashboardUser, DashboardRole, 
    Guid, DashboardUserClaim, DashboardUserRole, DashboardUserLogin,
    DashboardRoleClaim, DashboardUserToken>
{
    public DashboardIdentityDbContext(DbContextOptions<DashboardIdentityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
