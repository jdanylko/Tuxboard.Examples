using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class TuxboardUserLogin : IdentityUserLogin<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
}