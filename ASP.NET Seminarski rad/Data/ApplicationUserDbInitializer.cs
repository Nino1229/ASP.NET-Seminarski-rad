using Microsoft.AspNetCore.Identity;

namespace ASP.NET_Seminarski_rad.Data
{
    public static class ApplicationUserDbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "admin@admin.hr",
                Email = "admin@admin.hr"
            };

            var result = userManager.CreateAsync(user, "Lozinka.123.").Result;

            if (result.Succeeded) 
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}
