using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardRoleManager(
    IRoleStore<DashboardRole> store,
    IEnumerable<IRoleValidator<DashboardRole>> roleValidators,
    ILookupNormalizer keyNormalizer,
    IdentityErrorDescriber errors,
    ILogger<DashboardRoleManager> logger)
    : RoleManager<DashboardRole>(store, roleValidators, keyNormalizer, errors, logger);
