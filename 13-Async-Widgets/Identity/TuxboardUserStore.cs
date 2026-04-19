using AsyncWidgets.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AsyncWidgets.Identity;

public class TuxboardUserStore: UserStore<TuxboardUser, TuxboardRole, TuxboardRoleDbContext, Guid, 
    TuxboardUserClaim,TuxboardUserRole, TuxboardUserLogin, TuxboardUserToken, TuxboardRoleClaim>
{
    public TuxboardUserStore(TuxboardRoleDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }
}