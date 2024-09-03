using CreatingWidgets.Data.Context;
using CreatingWidgets.Identity;
using CreatingWidgets.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tuxboard.Core.Configuration;
using Tuxboard.Core.Data.Context;
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
        x => x.MigrationsAssembly("12-Creating-Widgets"));
});

// Inherited...the NEW Tuxboard DbContext
builder.Services.AddDbContext<TuxboardRoleDbContext>(options =>
{
    options.UseSqlServer(appConfig.ConnectionString,
        x => x.MigrationsAssembly("12-Creating-Widgets"));
});

// Attach Identity to the new Tuxboard Context
builder.Services.AddIdentity<TuxboardUser, TuxboardRole>()
    .AddEntityFrameworkStores<TuxboardRoleDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();

// For Dependency Injection
builder.Services.AddTransient<IDashboardService, DashboardService>();
builder.Services.AddTransient<IRoleDashboardService, RoleDashboardService>();
builder.Services.AddTransient<IWidgetRoleService, WidgetRoleService>();
builder.Services.AddTransient<ITuxDbContext, TuxDbContext>();
builder.Services.AddTransient<ITuxboardRoleDbContext, TuxboardRoleDbContext>();
builder.Services.AddTransient<IUserStore<TuxboardUser>, TuxboardUserStore>();
builder.Services.AddTransient<IRoleStore<TuxboardRole>, TuxboardRoleStore>();
builder.Services.AddTransient<SignInManager<TuxboardUser>>();
builder.Services.AddTransient<UserManager<TuxboardUser>>();

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
