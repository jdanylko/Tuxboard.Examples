using DefaultDashboards.Data.Context;
using DefaultDashboards.Identity;
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

// Tuxboard DbContext
builder.Services.AddDbContext<TuxDbContext>(options =>
{
    options.UseSqlServer(appConfig.ConnectionString,
        x => x.MigrationsAssembly("10-Default-Dashboards"));
});

// the NEW Tuxboard DbContext
builder.Services.AddDbContext<TuxboardRoleDbContext>(options =>
{
    options.UseSqlServer(appConfig.ConnectionString,
        x => x.MigrationsAssembly("10-Default-Dashboards"));
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<DashboardIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<DashboardUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DashboardIdentityDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();

// For Dependency Injection
builder.Services.AddTransient<IDashboardService, DashboardService>();
builder.Services.AddTransient<ITuxDbContext, TuxDbContext>();
builder.Services.AddTransient<ITuxboardRoleDbContext, TuxboardRoleDbContext>();
builder.Services.AddTransient<IRoleDashboardService, RoleDashboardService>();

builder.Services.AddTransient<DashboardRoleManager>();
builder.Services.AddTransient<DashboardUserManager>();

builder.Services.AddTransient<RoleManager<DashboardRole>, DashboardRoleManager>();
builder.Services.AddTransient<UserManager<DashboardUser>, DashboardUserManager>();
builder.Services.AddTransient<DashboardSignInManager>();

builder.Services.AddTransient<IUserStore<DashboardUser>, DashboardUserStore>();
builder.Services.AddTransient<IRoleStore<DashboardRole>, DashboardRoleStore>();

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
