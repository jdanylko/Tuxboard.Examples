using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Tuxboard.Core.Configuration;
using Tuxboard.Core.Domain.Entities;
using Tuxboard.Core.Infrastructure.Services;

namespace WidgetsExample.Pages;

public class IndexModel : PageModel
{
    private readonly IDashboardService<Guid> _service;
    private readonly TuxboardConfig _config;

    public Dashboard<Guid>? Dashboard { get; set; }

    public IndexModel(
        IDashboardService<Guid> service,
        IOptions<TuxboardConfig> options)
    {
        _service = service;
        _config = options.Value;
    }

    public async Task OnGet()
    {
        Dashboard = await _service.GetDashboardAsync(_config);
    }
}