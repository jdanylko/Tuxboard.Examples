using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardUserToken : IdentityUserToken<Guid>
{
    public virtual DashboardUser User { get; set; } = default!;
}