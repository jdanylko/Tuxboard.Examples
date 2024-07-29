using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardUserRole : IdentityUserRole<Guid>
{
    public virtual DashboardUser User { get; set; }
    public virtual DashboardRole Role { get; set; }
}
