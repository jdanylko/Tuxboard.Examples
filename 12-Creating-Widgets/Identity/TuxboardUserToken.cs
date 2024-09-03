using Microsoft.AspNetCore.Identity;

namespace CreatingWidgets.Identity;

public class TuxboardUserToken : IdentityUserToken<Guid>
{
    public virtual TuxboardUser User { get; set; } = default!;
}