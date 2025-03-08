using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using shoeVerse.Data;
using shoeVerse.Models;
using shoeVerse.Areas.Identity.Data;
using System;
using System.Threading.Tasks;
using shoeVerse;

var builder = WebApplication.CreateBuilder(args);


// Configure Database Connections
var connectionString = builder.Configuration.GetConnectionString("shoeVerseContext") ??
    throw new InvalidOperationException("Connection string 'shoeVerseContext' not found.");

builder.Services.AddDbContext<shoeVerseContext>(options =>
    options.UseSqlServer(connectionString));

var identityConnectionString = builder.Configuration.GetConnectionString("shoeVerseIdentityDbContextConnection") ??
    throw new InvalidOperationException("Connection string 'shoeVerseIdentityDbContextConnection' not found.");

builder.Services.AddDbContext<shoeVerseIdentityDbContext>(options =>
    options.UseSqlServer(identityConnectionString));

// Configure Identity Services
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<shoeVerseContext>();

// Add Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("GuestOnly", policy => policy.RequireRole("Guest"));
});

// Add MVC and Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// **Seed Roles & Admin Account**
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await SeedRolesAndAdmin.Initialize(services, userManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during role seeding: {ex.Message}");
    }
}

// Configure Middleware Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Configure Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Required for Identity UI

await app.RunAsync();
