using CreatingWidgets.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CreatingWidgets.Identity;

public class TuxboardUserStore(TuxboardRoleDbContext context, IdentityErrorDescriber? describer = null)
    : UserStore<TuxboardUser, TuxboardRole, TuxboardRoleDbContext, Guid,
        TuxboardUserClaim, TuxboardUserRole, TuxboardUserLogin, TuxboardUserToken, TuxboardRoleClaim>(context,
        describer);