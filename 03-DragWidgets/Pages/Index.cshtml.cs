using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Net;
using Tuxboard.Core.Configuration;
using Tuxboard.Core.Domain.Entities;
using Tuxboard.Core.Infrastructure.Models;
using Tuxboard.Core.Infrastructure.Services;

namespace DragWidgets.Web.Pages;

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

    public async Task<IActionResult> OnPostSaveWidgetPosition([FromBody] PlacementParameter model)
    {
        var placement = await _service.SaveWidgetPlacementAsync(model);

        if (placement == null)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError,
                $"Widget Placement (id:{model.PlacementId}) was NOT saved.");
        }

        return new OkObjectResult("Widget Placement was saved.");
    }
}