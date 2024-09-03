using Microsoft.AspNetCore.Identity;

namespace CreatingWidgets.Identity;

public class TuxboardRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual TuxboardRole Role { get; set; } = default!;
}