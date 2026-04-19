using Microsoft.AspNetCore.Identity;

namespace AsyncWidgets.Identity;

public class TuxboardRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual TuxboardRole Role { get; set; } = default!;
}