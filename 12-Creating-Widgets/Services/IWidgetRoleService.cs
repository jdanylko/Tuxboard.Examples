using CreatingWidgets.Identity;
using Tuxboard.Core.Domain.Entities;

namespace CreatingWidgets.Services;

public interface IWidgetRoleService
{
    Task<List<Widget>> GetWidgetsByRoleAsync(TuxboardUser user);
    Task<List<Widget>> GetDefaultWidgetsAsync();
}