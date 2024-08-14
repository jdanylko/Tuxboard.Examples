using DefaultDashboards.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DefaultDashboards.Identity;

public class DashboardRoleStore: RoleStore<DashboardRole, TuxboardRoleDbContext, Guid, DashboardUserRole, DashboardRoleClaim>
{
    public DashboardRoleStore(TuxboardRoleDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }
}
