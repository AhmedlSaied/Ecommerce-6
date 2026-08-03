using Ecommerce.Domain.Contracts;
using Ecommerce.infrastructure.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.infrastructure.Identity.Data
{
    public class IdentityDataSeeder(DbIdentityContext context,UserManager <ApplicationUser> userManager,RoleManager<IdentityRole> roleManager, ILogger<IdentityDataSeeder> Logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            var migrations= await context.Database.GetPendingMigrationsAsync(ct);
            if (migrations.Any())
            {
                await context.Database.MigrateAsync();
            }

            if (! await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if (! await userManager.Users.AnyAsync())
            {
                var Admin = new ApplicationUser()
                {
                    DisplayName = "Abdelrahman Shoaib",
                    Email = "Abdelrahman@gnmail.com",
                    UserName = "Abdelrahman26",
                    PhoneNumber = "01062501255"
                };
                var result= await userManager.CreateAsync(Admin,"P@ssw0rd");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(Admin, "SuperAdmin");
                }
                else
                {
                    var Errors =string.Join(", ", result.Errors);
                    Logger.LogWarning(Errors);
                }
            }

            

        }
    }
}
