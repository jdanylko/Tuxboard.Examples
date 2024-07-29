using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DefaultDashboards.Identity;

public class DashboardSignInManager(
    UserManager<DashboardUser> userManager,
    IHttpContextAccessor contextAccessor,
    IUserClaimsPrincipalFactory<DashboardUser> claimsFactory,
    IOptions<IdentityOptions> optionsAccessor,
    ILogger<DashboardSignInManager> logger,
    IAuthenticationSchemeProvider schemes,
    IUserConfirmation<DashboardUser> confirmation)
    : SignInManager<DashboardUser>(userManager, contextAccessor, claimsFactory,
        optionsAccessor, logger, schemes, confirmation);
