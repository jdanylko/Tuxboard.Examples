using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class TuxboardRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual TuxboardRole Role { get; set; } = default!;
}