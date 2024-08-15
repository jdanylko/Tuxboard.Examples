using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class TuxboardUserToken : IdentityUserToken<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
}