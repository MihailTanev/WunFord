namespace WunFord.Middleware
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using WunFord.Common;
    using WunFord.Data;
    using WunFord.Data.Models;

    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var dbContext = serviceProvider.GetService<WunFordDbContext>();

            if (!dbContext.Roles.Any())
            {
                await this.SeedDatabaseWithRoles(userManager, roleManager);
            }

            if (!dbContext.Statuses.Any())
            {
                dbContext.Statuses.Add(new Status { Name = "Dispatched" });
                dbContext.Statuses.Add(new Status { Name = "Awaiting approval" });
                dbContext.Statuses.Add(new Status { Name = "Change IP Typology" });
                dbContext.Statuses.Add(new Status { Name = "IP Typology change in Progress" });
            }

            dbContext.SaveChanges();

            await this.next(context);
        }

        private async Task SeedDatabaseWithRoles(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(RoleConstants.ADMIN_ROLE));
            await roleManager.CreateAsync(new IdentityRole(RoleConstants.COORDINATOR_ROLE));
            await roleManager.CreateAsync(new IdentityRole(RoleConstants.SPECIALIST_ROLE));

            var adminUserName = RoleConstants.ADMIN_ROLE;           

            var admin = await userManager.FindByNameAsync(adminUserName);

            if (admin == null)
            {
                admin = new User
                {
                    Email = "admin@user.com",
                    UserName = adminUserName,
                };

                await userManager.CreateAsync(admin, "admin123");

                await userManager.AddToRoleAsync(admin, adminUserName);
            }
            
        }
    }
}
