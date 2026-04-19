using Microsoft.AspNetCore.Identity;

namespace AsyncWidgets.Identity;

public class TuxboardUserToken : IdentityUserToken<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
}