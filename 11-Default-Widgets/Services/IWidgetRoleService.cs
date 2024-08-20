using DefaultWidgets.Identity;
using Tuxboard.Core.Domain.Entities;

namespace DefaultWidgets.Services;

public interface IWidgetRoleService
{
    Task<List<Widget>> GetWidgetsByRoleAsync(TuxboardUser user);
    Task<List<Widget>> GetDefaultWidgets();
}