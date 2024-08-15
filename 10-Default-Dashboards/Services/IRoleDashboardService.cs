using DefaultDashboards.Identity;
using Tuxboard.Core.Domain.Entities;

namespace DefaultDashboards.Services;

public interface IRoleDashboardService
{
    Task<DashboardDefault> GetDashboardTemplateByRoleAsync(TuxboardUser user);
    Task<bool> DashboardExistsForAsync(Guid userId);
}