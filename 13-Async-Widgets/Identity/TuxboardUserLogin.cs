using Microsoft.AspNetCore.Identity;

namespace AsyncWidgets.Identity;

public class TuxboardUserLogin : IdentityUserLogin<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
}