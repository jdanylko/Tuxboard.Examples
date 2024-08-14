using DefaultDashboards.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DefaultDashboards.Identity;

public class DashboardUserStore: UserStore<DashboardUser, DashboardRole, TuxboardRoleDbContext, Guid, 
    DashboardUserClaim,DashboardUserRole, DashboardUserLogin, DashboardUserToken, DashboardRoleClaim>
{
    public DashboardUserStore(TuxboardRoleDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }
}