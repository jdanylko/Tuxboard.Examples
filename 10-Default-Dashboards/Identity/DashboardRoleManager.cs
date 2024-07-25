using Microsoft.AspNetCore.Identity;

namespace DefaultDashboards.Identity;

public class DashboardRoleManager: RoleManager<DashboardRole>
{
    public DashboardRoleManager(
        IRoleStore<DashboardRole> store, 
        IEnumerable<IRoleValidator<DashboardRole>> roleValidators, 
        ILookupNormalizer keyNormalizer, 
        IdentityErrorDescriber errors, 
        ILogger<DashboardRoleManager> logger) 
        : base(store, roleValidators, keyNormalizer, errors, logger)
    {
    }
}