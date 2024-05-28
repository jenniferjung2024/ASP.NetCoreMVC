using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.Models.DataLayer.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => {
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

// Add session state to services for the app
builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SportsProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SportsPro")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
}).AddEntityFrameworkStores<SportsProContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    await ConfigureIdentity.CreateAdminUserAsync(scope.ServiceProvider);
}

// Identify session state service before mapping any routes
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
