using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class TuxboardUserClaim : IdentityUserClaim<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
}