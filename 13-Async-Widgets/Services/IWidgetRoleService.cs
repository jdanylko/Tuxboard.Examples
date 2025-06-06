using AsyncWidgets.Identity;
using Tuxboard.Core.Domain.Entities;

namespace AsyncWidgets.Services;

public interface IWidgetRoleService
{
    Task<List<Widget>> GetWidgetsByRoleAsync(TuxboardUser user);
    Task<List<Widget>> GetDefaultWidgetsAsync();
}