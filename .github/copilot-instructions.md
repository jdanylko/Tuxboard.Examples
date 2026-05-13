# Tuxboard Examples — Copilot Instructions

## Repository Overview

This is a **sequential tutorial repository** for [Tuxboard](https://github.com/jdanylko/Tuxboard), a lightweight ASP.NET Core dashboard library. Each numbered folder (`01-SimpleDashboard` → `14-Template`) is a standalone project that builds on the previous example. Changes to one project are intentionally not back-ported to earlier ones.

- Each project is a self-contained ASP.NET Core 8 Razor Pages app with its own SQL Server database, EF Core migrations, and front-end build pipeline.
- When asked to make changes, **stay in the specific project folder** unless explicitly told otherwise.
- Projects are **not back-ported** — each folder is a snapshot of that tutorial step.

---

## Build Commands

All front-end build commands run from the **individual project folder** (e.g., `13-Async-Widgets/`):

```bash
# Compile TypeScript → bundle JS, compile SCSS → CSS
npx gulp build

# Clean output artifacts
npx gulp clean
```

The Gulp pipeline: TypeScript → transpiled JS (via `gulp-typescript`) → Browserify bundle → Babelified → minified as `wwwroot/js/dashboard.min.js`.

`tsconfig.json` targets **ES6 / ES2015 modules** with `moduleResolution: node` and `skipLibCheck: true`.

For .NET:
```bash
dotnet build
dotnet run

# Full solution build (use --no-incremental to surface all warnings)
dotnet build Tuxboard.Examples.sln --no-incremental

# EF Core migrations (run from project folder)
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

> **Gulp build quirk:** Running multiple sequential `gulp build` calls via `&&` in PowerShell tends to hang. Run each project's build as a separate process: `cmd /c "cd /d <ProjectFolder> && npx gulp build"`.

---

## Architecture

### Dashboard Object Hierarchy (Server-Side)

```
Dashboard<Guid>
  └── Tabs[]
        └── Layouts[]
              └── LayoutRows[]  (each with a LayoutType defining column count)
                    └── Columns[]
                          └── WidgetPlacements[]  (data-id = WidgetPlacementId GUID)
```

`Dashboard<Guid>` and all core domain entities come from the **`Tuxboard.Core`** NuGet package. Projects do not redefine these — they extend them.

### Async Widget Loading Pattern

The key pattern in the later examples (12+) is a **two-phase render**:

1. **Server (OnGet):** The `TuxboardTemplate` view component renders the dashboard shell. Each widget renders only its outer `.card` frame (via the `WidgetTemplate` view component) with an empty `.card-body` and a visible `.overlay` spinner. This is controlled by `WidgetPlacement.UseTemplate == true`.

2. **Client (initialize):** `Tuxboard.updateWidgets()` calls `Promise.all(widgets.map(w => updateWidget(w)))` — all widgets fire concurrently. Each calls `OnPostGetWidget` with the placement ID, which returns `ViewComponent(widget.Widget.Name, new { placement = widget })` — the actual widget HTML — injected into `.card-body`.

If `WidgetPlacement.UseTemplate == false`, the widget ViewComponent is rendered server-side inline (no async loading).

### DbContext Inheritance

Projects extend `TuxDbContext<Guid>` from Tuxboard.Core:

```csharp
public class TuxboardRoleDbContext : TuxDbContext<Guid>, ITuxboardRoleDbContext
```

Both the base `TuxDbContext<Guid>` and the derived context are registered in DI. EF migrations use `x => x.MigrationsAssembly("ProjectFolderName")`.

### Configuration

`TuxboardConfig` is bound from `appsettings.json`:

```json
"TuxboardConfig": {
  "ConnectionString": "...",
  "CreateSeedData": true,
  "Schema": "dbo"
}
```

Injected via `IOptions<TuxboardConfig>` in page models.

---

## Key Conventions

### Razor Pages Handlers ↔ TypeScript URLs

All postbacks use the `?handler=HandlerName` Razor Pages pattern. The TypeScript service layer maps directly:

| TypeScript URL constant | Razor Pages handler |
|---|---|
| `?handler=GetWidget` | `OnPostGetWidgetAsync` |
| `?handler=Refresh` | `OnPostRefresh` |
| `?handler=SaveWidgetPosition` | `OnPostSaveWidgetPosition` |

Handler names in C# (`OnPost{Name}[Async]`) must match the `?handler=` value in `TuxboardService.ts`.

### Widget ViewComponents

Each widget is an ASP.NET Core ViewComponent in `Pages/Shared/Components/{WidgetName}/`:

```csharp
[ViewComponent(Name = "helloworld")]   // lowercase, no spaces
public class HelloWorldViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(WidgetPlacement placement) { ... }
}
```

- The `Name` in `[ViewComponent(Name = "...")]` must **exactly match** the value stored in the `Widget.Name` database column.
- The view is always `Default.cshtml` in the same folder.
- Widget ViewComponents receive a `WidgetPlacement` parameter.
- `OnPostGetWidgetAsync` in `Index.cshtml.cs` must pass the placement as a named argument: `return ViewComponent(widget.Widget.Name, new { placement = widget })`. Omitting the second argument causes `null` to be passed to `Invoke`, breaking all widget renders.
- For async widgets, implement `InvokeAsync(WidgetPlacement placement)` (not `Invoke`). Use `await Task.Delay(...)` — never `Thread.Sleep(...)`, which blocks the thread pool.

### Widget GroupName

Widgets in the database are filtered by `GroupName`. The default group used across examples is `"Example"`. Role-specific widget filtering queries against this field in `WidgetRoleService`.

### TypeScript Selectors

All DOM selectors are centralized in `wwwroot/src/tuxboard/common.ts`. Widget DOM structure maps to Bootstrap card classes:

| Concept | CSS Selector |
|---|---|
| Dashboard container | `.dashboard` |
| Widget | `.card` (with `data-id="<placementGuid>"`) |
| Widget header | `.card-header` |
| Widget body | `.card-body` |
| Async loader overlay | `.overlay` |
| Collapsed state | CSS class `collapsed` on `.card` |

The `data-id` attribute carries entity GUIDs throughout the DOM and is referenced via `dataIdAttribute = "data-id"` in TypeScript.

### Service Layer (TypeScript)

`BaseService` → `TuxboardService`. All service methods are `async/await` returning `Promise<string>` (HTML fragments) or `Promise<Response>`. Use `try/catch` in service methods; do not use `.then().catch()` chains.

- `logError` in `BaseService` must actively call `console.error(...)` — never leave it commented out, or errors will be silently swallowed.
- `logError` is a `protected` method on `BaseService`. The `Tuxboard` class does **not** extend `BaseService`, so `.catch(this.logError)` inside `tuxboard.ts` is a TypeScript error. Use an inline arrow: `.catch((err: Error) => console.error("Issue w/ fetch call: \n", err))`.

### Widget Toolbar Event Delegation

Do **not** use `querySelectorAll` + `addEventListener` on each toolbar button call — this accumulates duplicate listeners on every re-render. Instead, use a single delegated listener on the dashboard container stored as a named class field so it can be properly removed and re-added:

```typescript
private handleWidgetToolbarClick = (e: Event) => {
    const target = e.target as HTMLElement;
    const button = target.closest<HTMLButtonElement>(".toolbar-button");
    if (!button) return;
    // handle button...
};

attachToolbarEvents() {
    this.dashboard.removeEventListener("click", this.handleWidgetToolbarClick);
    this.dashboard.addEventListener("click", this.handleWidgetToolbarClick);
}
```

Anonymous functions cannot be removed via `removeEventListener` — always store the handler as a named field.

### Razor Conditional Rendering

The `condition=""` attribute is a custom tag helper used throughout `.cshtml` files:

```html
<h3 condition="!Model.HasDashboard">Register or login to view your dashboard.</h3>
<vc:tuxboardtemplate model="Model.Dashboard"></vc:tuxboardtemplate>
```

### Identity

Later projects (09+) use a fully custom ASP.NET Identity implementation:
- `TuxboardUser`, `TuxboardRole` extend the Identity base classes
- Custom stores (`TuxboardUserStore`, `TuxboardRoleStore`) are registered as `Transient`
- `TuxboardRoleDbContext` handles both Tuxboard tables and Identity tables in one context
- Projects 09–13 **must** include `app.UseAuthentication()` **before** `app.UseAuthorization()` in the middleware pipeline. Without it, Identity cookies are never read and logins silently fail even though auth services are registered.

#### Razor Pages Authorization Constraint

`[Authorize]` and `[AllowAnonymous]` **cannot** be applied to individual Razor Page handler methods (`OnGet`, `OnPost`, etc.) — this produces MVC1001 warnings and has no effect. They only work at the page model **class** level. To conditionally protect behavior without restricting `OnGet`, use `User.Identity.IsAuthenticated` checks inside the handler.

### DI Registration Pattern

All services are registered as **`AddScoped`** (not `AddTransient` — scoped is correct for EF Core `DbContext`-backed services within a request):

```csharp
builder.Services.AddScoped<IDashboardService<Guid>, DashboardService<Guid>>();
builder.Services.AddScoped<ITuxDbContext<Guid>, TuxDbContext<Guid>>();
```

### Docker

Each project has a `Dockerfile`. The database connection string is passed via environment variable:

```
TUXBOARDCONFIG__CONNECTIONSTRING=Data Source=...
```

Use a `development.env` file (gitignored) to store local credentials.
