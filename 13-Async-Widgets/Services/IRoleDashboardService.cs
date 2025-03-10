using AsyncWidgets.Identity;
using Tuxboard.Core.Domain.Entities;

namespace AsyncWidgets.Services;

public interface IRoleDashboardService
{
    Task<DashboardDefault> GetDashboardTemplateByRoleAsync(TuxboardUser user);
    Task<bool> DashboardExistsForAsync(Guid userId);
}