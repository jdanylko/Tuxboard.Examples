using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DefaultDashboards.Identity;

public class DashboardUserManager: UserManager<DashboardUser>
{
    public DashboardUserManager(
        IUserStore<DashboardUser> store, 
        IOptions<IdentityOptions> optionsAccessor, 
        IPasswordHasher<DashboardUser> passwordHasher, 
        IEnumerable<IUserValidator<DashboardUser>> userValidators, 
        IEnumerable<IPasswordValidator<DashboardUser>> passwordValidators, 
        ILookupNormalizer keyNormalizer, 
        IdentityErrorDescriber errors, 
        IServiceProvider services, 
        ILogger<DashboardUserManager> logger) 
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, 
            keyNormalizer, errors, services, logger)
    {
    }
}