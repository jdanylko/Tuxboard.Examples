using DefaultDashboards.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DefaultDashboards.Identity;

public class DashboardRoleStore: RoleStore<DashboardRole, DashboardIdentityDbContext, Guid>
{
    public DashboardRoleStore(DashboardIdentityDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }
}