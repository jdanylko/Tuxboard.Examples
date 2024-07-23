using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DefaultDashboards.Identity;

public class DashboardIdentityUserStore(DashboardIdentityContext context, IdentityErrorDescriber? describer = null)
    : UserStore<DashboardIdentityUser, DashboardIdentityRole, DashboardIdentityContext, Guid>(context, describer);