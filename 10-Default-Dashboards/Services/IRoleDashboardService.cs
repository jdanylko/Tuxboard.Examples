using DefaultDashboards.Identity;
using Tuxboard.Core.Domain.Entities;

namespace DefaultDashboards.Services;

public interface IRoleDashboardService
{
    Task<DashboardDefault> GetDashboardTemplateByRoleAsync(DashboardUser user);
    Task<bool> DashboardExistsForAsync(Guid userId);
}