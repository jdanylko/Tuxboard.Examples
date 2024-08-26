﻿using DefaultWidgets.Identity;
using DefaultWidgets.Models;
using Microsoft.EntityFrameworkCore;
using Tuxboard.Core.Data.Context;

namespace DefaultWidgets.Data.Context;

public interface ITuxboardRoleDbContext: ITuxDbContext
{
    DbSet<RoleDefaultDashboard> RoleDefaultDashboards { get; set; }
    DbSet<WidgetRole> WidgetRoles { get; set; }

    // Identity
    DbSet<TuxboardUserClaim> TuxboardUserClaims { get; set; }
    DbSet<TuxboardUserRole> TuxboardUserRoles { get; set; }
    DbSet<TuxboardUserLogin> TuxboardUserLogins { get; set; }
    DbSet<TuxboardUserToken> TuxboardUserTokens { get; set; }
    DbSet<TuxboardUser> TuxboardUsers { get; set; }
    DbSet<TuxboardRole> TuxboardRoles { get; set; }
    DbSet<TuxboardRoleClaim> TuxboardRoleClaims { get; set; }


}