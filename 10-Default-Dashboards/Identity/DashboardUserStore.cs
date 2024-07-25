using DefaultDashboards.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DefaultDashboards.Identity;

public class DashboardUserStore: UserStore<DashboardUser, DashboardRole, DashboardIdentityDbContext, Guid>
{
    public DashboardUserStore(DashboardIdentityDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }
}