using System.Net;
using System.Security.Claims;
using DefaultDashboards.Extensions;
using DefaultDashboards.Identity;
using DefaultDashboards.Models;
using DefaultDashboards.Pages.Shared.Components.AddWidgetDialog;
using DefaultDashboards.Pages.Shared.Components.AdvancedLayoutDialog;
using DefaultDashboards.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Tuxboard.Core.Configuration;
using Tuxboard.Core.Domain.Entities;
using Tuxboard.Core.Infrastructure.Models;
using Tuxboard.Core.Infrastructure.Services;

namespace DefaultDashboards.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IDashboardService<Guid> _service;
    private readonly IRoleDashboardService _roleDashboardService;
    private readonly UserManager<TuxboardUser> _userManager;
    private readonly TuxboardConfig _config;

    public Dashboard<Guid> Dashboard { get; set; } = null!;
    public bool HasDashboard => Dashboard != null;

    public IndexModel(
        ILogger<IndexModel> logger,
        IDashboardService<Guid> service,
        IRoleDashboardService roleDashboardService,
        UserManager<TuxboardUser> userManager,
        IOptions<TuxboardConfig> options)
    {
        _logger = logger;
        _service = service;
        _roleDashboardService = roleDashboardService;
        _userManager = userManager;
        _config = options.Value;
    }

    public async Task OnGet()
    {
        var id = GetIdentity();
        if (id != Guid.Empty)
        {
            Dashboard = await _service.DashboardExistsForAsync(id)
                ? await _service.GetDashboardForAsync(_config, id)
                : await GetDashboardByRole(id);
        }
    }

    private async Task<Dashboard<Guid>> GetDashboardByRole(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        // If we can't find the user, load the default dashboard.
        if (user == null) 
            return await _service.GetDashboardAsync(_config);

        var template = await _roleDashboardService.GetDashboardTemplateByRoleAsync(user);
        await _service.CreateDashboardFromAsync(template, id);

        return await _service.GetDashboardForAsync(_config, id);
    }

    private Guid GetIdentity()
    {
        var claim = User?.FindFirst(ClaimTypes.NameIdentifier);
        return claim != null 
            ? new Guid(claim.Value) 
            : Guid.Empty;
    }
    
    /* Postbacks */

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

    public async Task<IActionResult> OnPostRefresh()
    {
        var id = GetIdentity();

        var dashboard = await _service.GetDashboardForAsync(_config, id);

        return ViewComponent("tuxboardtemplate", dashboard);
    }

    public async Task<IActionResult> OnPostRemoveWidgetAsync([FromBody] RemoveWidgetRequest request)
    {
        var id = GetIdentity();

        var dashboard = await _service.GetDashboardForAsync(_config, id);

        // Use this as a way to identify a widget placement IN A DASHBOARD.
        var placement = dashboard.GetCurrentTab().GetWidgetPlacements()
            .FirstOrDefault(e => e.WidgetPlacementId == request.WidgetId);

        if (placement == null)
            return new NotFoundResult();

        await _service.RemoveWidgetAsync(placement.WidgetPlacementId);

        return new OkResult();
    }

    public async Task<IActionResult> OnPostSetWidgetStateAsync([FromBody] WidgetStateRequest request)
    {
        var widget = await _service.UpdateCollapsedAsync(request.WidgetPlacementId, request.Collapsed);
        return widget != null
            ? new OkResult()
            : new NotFoundResult();
    }


    /* Dialogs */

    /* Simple Layout Dialog */
    public async Task<IActionResult> OnPostSimpleLayoutDialog()
    {
        var id = GetIdentity();

        var dashboard = await _service.GetDashboardForAsync(_config, id);
        var layouts = dashboard.GetCurrentTab().GetLayouts().FirstOrDefault();
        var currentLayout = layouts?.LayoutRows.FirstOrDefault();

        var layoutTypes = await _service.GetLayoutTypesAsync();
        var result = layoutTypes.Select(e => e.ToDto(currentLayout!.LayoutTypeId)).ToList();

        return ViewComponent("simplelayoutdialog", result);
    }

    public async Task<IActionResult> OnPostSaveSimpleLayoutAsync([FromBody] SimpleLayoutRequest request)
    {
        var id = GetIdentity();

        var dashboard = await _service.GetDashboardForAsync(_config, id);

        // Since we only have a single layout for this example, we can grab the first one.
        var tab = dashboard.GetCurrentTab();
        var layouts = tab.GetLayouts().FirstOrDefault();

        // LayoutRows have a LayoutType. If we change the LayoutType, the LayoutRow will update on next reload.
        var currentLayoutRow = layouts?.LayoutRows?.FirstOrDefault();
        if (currentLayoutRow != null)
        {
            await _service.ChangeLayoutRowToAsync(currentLayoutRow, request.LayoutTypeId);
        }

        // Refresh
        dashboard = await _service.GetDashboardForAsync(_config, id);

        return ViewComponent("tuxboardtemplate", dashboard);
    }

    /* Advanced Layout Dialog */

    public async Task<IActionResult> OnPostAdvancedLayoutDialog()
    {
        var id = GetIdentity();

        var layoutRows = new List<LayoutRow>();

        var dashboard = await _service.GetDashboardForAsync(_config, id);
        var layouts = dashboard.GetCurrentTab().GetLayouts().FirstOrDefault();
        if (layouts != null)
        {
            layoutRows.AddRange(layouts.LayoutRows.ToList());
        }

        var layoutTypes = await _service.GetLayoutTypesAsync();

        return ViewComponent("advancedlayoutdialog", new AdvancedLayoutModel
        {
            LayoutRows = layoutRows.ToList(),
            LayoutTypes = layoutTypes
        });
    }

    public async Task<IActionResult> OnPostGetLayoutTypeAsync([FromBody] LayoutTypeRequest request)
    {
        var layoutTypes = await _service.GetLayoutTypesAsync();
        var layoutType = layoutTypes.FirstOrDefault(e => e.LayoutTypeId == request.Id);

        return ViewComponent("advancedlayoutrow", new LayoutRow
        {
            LayoutRowId = Guid.Empty,
            LayoutTypeId = request.Id,
            RowIndex = 0,
            LayoutType = layoutType
        });
    }

    public async Task<IActionResult> OnPostSaveAdvancedLayoutAsync([FromBody] AdvancedLayoutRequest request)
    {
        await _service.SaveLayoutAsync(request.TabId, request.LayoutList
            .Select(e =>
                new LayoutOrder
                {
                    LayoutRowId = e.LayoutRowId.Equals(Guid.Empty)
                        ? new Guid()
                        : e.LayoutRowId,
                    TypeId = e.TypeId,
                    Index = e.Index
                })
            .ToList());


        var id = GetIdentity();

        var dashboard = await _service.GetDashboardForAsync(_config, id);

        return ViewComponent("tuxboardtemplate", dashboard);
    }

    public async Task<IActionResult> OnPostCanDeleteLayoutRowAsync([FromBody] CanDeleteRequest request)
    {
        var result = await _service.CanDeleteLayoutRowAsync(request.TabId, request.LayoutRowId);

        return result
            ? new OkObjectResult(new CanDeleteResponse(request.LayoutRowId, string.Empty))
            : new ConflictObjectResult(new CanDeleteResponse(request.LayoutRowId,
                "Cannot delete Layout Row. Row contains widgets."));
    }

    /* Add Widget Dialog */

    public async Task<IActionResult> OnPostAddWidgetsDialog()
    {
        var widgets = (await _service.GetWidgetsAsync())
            .Select(r => r.ToDto())
            .ToList();

        return ViewComponent("addwidgetdialog", new AddWidgetModel { Widgets = widgets });
    }

    public async Task<IActionResult> OnPostAddWidgetAsync([FromBody] AddWidgetRequest request)
    {
        var id = GetIdentity();

        var dashboard = await _service.GetDashboardForAsync(_config, id);

        var baseWidget = await _service.GetWidgetAsync(request.WidgetId);

        var layoutRow = dashboard.GetFirstLayoutRow();
        if (layoutRow != null)
        {
            var placement = layoutRow.CreateFromWidget(baseWidget);
            // placement object can be set to any other layout row chosen;
            // default is first layout row, first column.
            await _service.AddWidgetPlacementAsync(placement);
        }

        return new OkResult();
    }
}