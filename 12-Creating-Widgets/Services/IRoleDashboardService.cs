using DefaultWidgets.Identity;
using Tuxboard.Core.Domain.Entities;

namespace DefaultWidgets.Services;

public interface IRoleDashboardService
{
    Task<DashboardDefault> GetDashboardTemplateByRoleAsync(TuxboardUser user);
    Task<bool> DashboardExistsForAsync(Guid userId);
}