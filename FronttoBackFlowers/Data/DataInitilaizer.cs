using FronttoBackFlowers.DAL;
using FronttoBackFlowers.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FronttoBackFlowers.Data
{
    public class DataInitilaizer
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager; 

        public DataInitilaizer(AppDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedData()
        {
            await _dbContext.Database.MigrateAsync();

            var Roles = new List<string> { RoleConstants.AdminRole, RoleConstants.UserRole };

            foreach (var role in Roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                    continue;

                var result = await _roleManager.CreateAsync(new IdentityRole { 
                    Name = role
                });

                if(!result.Succeeded)
                {
                    //logging
                }

                var user = new User
                {
                    Fullname = "Admin admin",
                    UserName = "admin",
                    Email = "admin@code.az"
                };
                
                if(await _userManager.FindByNameAsync(user.UserName)!= null)
                {
                    return;
                }

                await _userManager.CreateAsync(user, "12345@");
                await _userManager.AddToRoleAsync(user, RoleConstants.AdminRole);

            }
        }

    }
}
