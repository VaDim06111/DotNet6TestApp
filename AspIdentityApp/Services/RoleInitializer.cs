namespace AspIdentityApp.Services
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync(UserRoles.Admin) is null)
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (await roleManager.FindByNameAsync(UserRoles.User) is null)
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await userManager.FindByEmailAsync(DbConstants.AdminEmail) is null)
            {
                IdentityUser admin = new() { Email = DbConstants.AdminEmail, UserName = DbConstants.AdminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, DbConstants.AdminPassword);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, UserRoles.Admin);
            }

        }
    }
}
