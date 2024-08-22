using Microsoft.AspNetCore.Identity;

namespace DefaultWidgets.Identity;

public class TuxboardUserToken : IdentityUserToken<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
}