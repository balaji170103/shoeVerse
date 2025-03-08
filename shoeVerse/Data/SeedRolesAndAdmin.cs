using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace shoeVerse
{
    public static class SeedRolesAndAdmin
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Define roles
            string[] roleNames = { "Admin", "Guest" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Create the role if it doesn't exist
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create Admin User
            var adminUser = new IdentityUser
            {
                UserName = "admin@shoeverse.com",
                Email = "admin@shoeverse.com",
            };

            string adminPassword = "Admin@123";
            var user = await userManager.FindByEmailAsync(adminUser.Email);

            if (user == null)
            {
                var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdminUser.Succeeded)
                {
                    // Assign the Admin role to the admin user
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Create Guest User
            var guestUser = new IdentityUser
            {
                UserName = "guest@shoeverse.com",
                Email = "guest@shoeverse.com",
            };

            string guestPassword = "Guest@123";
            var guest = await userManager.FindByEmailAsync(guestUser.Email);

            if (guest == null)
            {
                var createGuestUser = await userManager.CreateAsync(guestUser, guestPassword);
                if (createGuestUser.Succeeded)
                {
                    // Assign the Guest role to the guest user
                    await userManager.AddToRoleAsync(guestUser, "Guest");
                }
            }
        }
    }
}