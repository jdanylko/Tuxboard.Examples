using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardUserRole : IdentityUserRole<Guid>
{
    public virtual DashboardUser User { get; set; } = default!;
    public virtual DashboardRole Role { get; set; } = default!;
}