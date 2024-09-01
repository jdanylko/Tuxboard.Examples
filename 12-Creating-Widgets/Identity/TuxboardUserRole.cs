using Microsoft.AspNetCore.Identity;

namespace DefaultWidgets.Identity;

public class TuxboardUserRole : IdentityUserRole<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
    public virtual TuxboardRole Role { get; set; } = default!;
}