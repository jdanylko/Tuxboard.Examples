using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardUserLogin : IdentityUserLogin<Guid>
{
    public virtual DashboardUser User { get; set; }
}