using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DefaultDashboards.Identity;

public class DashboardIdentityRoleStore(DashboardIdentityContext context, IdentityErrorDescriber? describer = null)
    : RoleStore<DashboardIdentityRole, DashboardIdentityContext, Guid>(context, describer);