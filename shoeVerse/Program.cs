using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using shoeVerse.Data;
using shoeVerse.Models;
using shoeVerse.Areas.Identity.Data;
using shoeVerse;

var builder = WebApplication.CreateBuilder(args);

// Configure Database
builder.Services.AddDbContext<shoeVerseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("shoeVerseContext") ??
    throw new InvalidOperationException("Connection string 'shoeVerseContext' not found.")));

// Add Identity Services
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()  // Enable Role Management
    .AddEntityFrameworkStores<shoeVerseContext>();

// Add Authorization Policies (Optional)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("GuestOnly", policy => policy.RequireRole("Guest"));
});

// Add MVC Controllers with Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// **Seed Roles & Admin Account**
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>(); // Get UserManager service

    // Seed Roles and Users
    await SeedRolesAndAdmin.Initialize(services, userManager); // Pass the userManager to the Initialize method
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Required for Identity UI

await app.RunAsync(); // Use RunAsync instead of Run to make the top-level statements asynchronous