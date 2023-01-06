using Lanchonete.Servicos.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Lanchonete.Servicos
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";

                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;

            }

            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";

                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;

            }
        }

        public void SeedUsers()
        {
            if (_userManager.FindByEmailAsync("usuario@localhost").Result is null)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = "usuario@localhost",
                    Email = "usuario@localhost",
                    NormalizedUserName = "USUARIO@LOCALHOST",
                    NormalizedEmail = "USUARIO@LOCALHOST",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2023").Result;

                if (result.Succeeded)
                    _userManager.AddToRoleAsync(user, "Member").Wait();

            }

            if (_userManager.FindByEmailAsync("admin@localhost").Result is null)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = "admin@localhost",
                    Email = "admin@localhost",
                    NormalizedUserName = "ADMIN@LOCALHOST",
                    NormalizedEmail = "ADMIN@LOCALHOST",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2023").Result;

                if (result.Succeeded)
                    _userManager.AddToRoleAsync(user, "Admin").Wait();

            }
        }
    }
}
