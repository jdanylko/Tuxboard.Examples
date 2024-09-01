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

// Tuxboard DbContext
builder.Services.AddDbContext<TuxDbContext>(options =>
{
    options.UseSqlServer(appConfig.ConnectionString,
        x => x.MigrationsAssembly("05-Layout-1"));
});

// Add services to the container.
builder.Services.AddRazorPages();

// For Dependency Injection
builder.Services.AddTransient<IDashboardService, DashboardService>();
builder.Services.AddTransient<ITuxDbContext, TuxDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
