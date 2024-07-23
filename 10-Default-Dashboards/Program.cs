using DefaultDashboards.Context;
using DefaultDashboards.Identity;
using Microsoft.EntityFrameworkCore;
using Tuxboard.Core.Configuration;
using Tuxboard.Core.Infrastructure.Interfaces;
using Tuxboard.Core.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Tuxboard Configuration
var appConfig = new TuxboardConfig();
builder.Configuration
    .GetSection(nameof(TuxboardConfig))
    .Bind(appConfig);

// Role-based Tuxboard DbContext to add RoleDashboard relationships
builder.Services.AddDbContext<RoleTuxboardContext>(options =>
{
    options.UseSqlServer(appConfig.ConnectionString,
        x => x.MigrationsAssembly("10-Default-Dashboards"));
});

builder.Services.AddDbContext<DashboardIdentityContext>(options =>
    options.UseSqlServer(appConfig.ConnectionString,
        x => x.MigrationsAssembly("10-Default-Dashboards")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<DashboardIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DashboardIdentityContext>();

builder.Services.AddRazorPages();

// For Dependency Injection
builder.Services.AddTransient<IDashboardService, DashboardService>();
// builder.Services.AddTransient<ITuxDbContext, RoleTuxboardContext>();
builder.Services.AddTransient<IRoleTuxboardContext, RoleTuxboardContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
