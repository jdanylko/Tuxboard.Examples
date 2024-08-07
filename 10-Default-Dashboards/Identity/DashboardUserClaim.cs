using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardUserClaim : IdentityUserClaim<Guid>
{
    public virtual DashboardUser User { get; set; } = default!;
}