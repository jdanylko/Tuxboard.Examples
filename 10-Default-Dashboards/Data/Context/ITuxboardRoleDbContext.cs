using DefaultDashboards.Identity;
using DefaultDashboards.Models;
using Microsoft.EntityFrameworkCore;
using Tuxboard.Core.Data.Context;

namespace DefaultDashboards.Data.Context;

public interface ITuxboardRoleDbContext: ITuxDbContext
{
    DbSet<RoleDefaultDashboard> RoleDefaultDashboards { get; set; }

    // Identity
    DbSet<DashboardUserClaim> DashboardUserClaims { get; set; }
    DbSet<DashboardUserRole> DashboardUserRoles { get; set; }
    DbSet<DashboardUserLogin> DashboardUserLogins { get; set; }
    DbSet<DashboardUserToken> DashboardUserTokens { get; set; }
    DbSet<DashboardUser> DashboardUsers { get; set; }
    DbSet<DashboardRole> DashboardRoles { get; set; }
    DbSet<DashboardRoleClaim> DashboardRoleClaims { get; set; }


}