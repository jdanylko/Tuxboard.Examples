using DefaultDashboards.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DefaultDashboards.Identity;

public class DashboardUserStore(DashboardIdentityDbContext context, IdentityErrorDescriber? describer = null)
    : UserStore<DashboardUser, DashboardRole, DashboardIdentityDbContext, Guid>(context, describer);
