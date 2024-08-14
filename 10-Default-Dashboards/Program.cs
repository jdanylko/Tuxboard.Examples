using DefaultDashboards.Data.Context;
using DefaultDashboards.Identity;
using DefaultDashboards.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tuxboard.Core.Configuration;
using Tuxboard.Core.Data.Context;
using Tuxboard.Core.Infrastructure.Interfaces;
using Tuxboard.Core.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Tuxboard Configuration
var appConfig = new TuxboardConfig();
builder.Configuration
    .GetSection(nameof(TuxboardConfig))
    .Bind(appConfig);

builder.Services.Configure<TuxboardConfig>(builder.Configuration.GetSection(nameof(TuxboardConfig)));

// Base DbContext
builder.Services.AddDbContext<TuxDbContext>(options =>
{
    options.UseSqlServer(appConfig.ConnectionString,
        x => x.MigrationsAssembly("10-Default-Dashboards"));
});

// Inherited...the NEW Tuxboard DbContext
builder.Services.AddDbContext<TuxboardRoleDbContext>(options =>
{
    options.UseSqlServer(appConfig.ConnectionString,
        x => x.MigrationsAssembly("10-Default-Dashboards"));
});

// Attach Identity to the new Tuxboard Context
builder.Services.AddIdentity<DashboardUser, DashboardRole>()
    .AddEntityFrameworkStores<TuxboardRoleDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();

// For Dependency Injection
builder.Services.AddTransient<IDashboardService, DashboardService>();
builder.Services.AddTransient<IRoleDashboardService, RoleDashboardService>();
builder.Services.AddTransient<ITuxDbContext, TuxDbContext>();
builder.Services.AddTransient<ITuxboardRoleDbContext, TuxboardRoleDbContext>();
builder.Services.AddTransient<IUserStore<DashboardUser>, DashboardUserStore>();
builder.Services.AddTransient<IRoleStore<DashboardRole>, DashboardRoleStore>();
builder.Services.AddTransient<SignInManager<DashboardUser>>();
builder.Services.AddTransient<UserManager<DashboardUser>>();

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
