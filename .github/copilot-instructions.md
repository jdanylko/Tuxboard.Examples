# Tuxboard Examples — Copilot Instructions

## Repository Overview

This is a **sequential tutorial repository** for [Tuxboard](https://github.com/jdanylko/Tuxboard), a lightweight ASP.NET Core dashboard library. Each numbered folder (`01-SimpleDashboard` → `14-Template`) is a standalone project that builds on the previous example. Changes to one project are intentionally not back-ported to earlier ones.

- Each project is a self-contained ASP.NET Core 8 Razor Pages app with its own SQL Server database, EF Core migrations, and front-end build pipeline.
- When asked to make changes, **stay in the specific project folder** unless explicitly told otherwise.

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

# EF Core migrations (run from project folder)
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

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

2. **Client (initialize):** `Tuxboard.updateWidgets()` calls `Promise.all(widgets.map(w => updateWidget(w)))` — all widgets fire concurrently. Each calls `OnPostGetWidget` with the placement ID, which returns `ViewComponent(widget.Widget.Name, placement)` — the actual widget HTML — injected into `.card-body`.

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
- `OnPostGetWidgetAsync` in `Index.cshtml.cs` calls `ViewComponent(widget.Widget.Name)` to dynamically invoke the correct component.

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

### DI Registration Pattern

All services are registered as `Transient`:

```csharp
builder.Services.AddTransient<IDashboardService<Guid>, DashboardService<Guid>>();
builder.Services.AddTransient<ITuxDbContext<Guid>, TuxDbContext<Guid>>();
```

### Docker

Each project has a `Dockerfile`. The database connection string is passed via environment variable:

```
TUXBOARDCONFIG__CONNECTIONSTRING=Data Source=...
```

Use a `development.env` file (gitignored) to store local credentials.
