using DefaultDashboards.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DefaultDashboards.Identity;

public class DashboardRoleStore(DashboardIdentityDbContext context, IdentityErrorDescriber? describer = null)
    : RoleStore<DashboardRole, DashboardIdentityDbContext, Guid, DashboardUserRole, DashboardRoleClaim>(context,
        describer);
