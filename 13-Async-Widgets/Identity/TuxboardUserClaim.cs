using Microsoft.AspNetCore.Identity;

namespace AsyncWidgets.Identity;

public class TuxboardUserClaim : IdentityUserClaim<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
}