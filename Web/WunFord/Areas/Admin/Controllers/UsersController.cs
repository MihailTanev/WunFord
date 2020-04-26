namespace WunFord.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using WunFord.Common;
    using WunFord.Data.Models;
    using WunFord.Data.ViewModels.User;
    using WunFord.Services.Interfaces;

    [Area(AreaConstants.Admin)]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IUsersService usersService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.usersService = usersService;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = this.usersService.GetAllUsers();

            return this.View(users);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            EditUserViewModel model = new EditUserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, EditUserViewModel model)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return this.View(model);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Username;

            if (ModelState.IsValid)
            {
                await this.userManager.UpdateAsync(user);

                return this.RedirectToAction(nameof(Index), new { area = AreaConstants.Admin });
            }
            else
            {
                return this.View(model);
            }
        }

        public async Task<IActionResult> ManageRole(string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var currentUserRole = await this.userManager.GetRolesAsync(user);

            var roles = currentUserRole
                .Select(role => new SelectListItem
                {
                    Text = role,
                    Value = role
                })
                .ToList();

            var model = new RoleViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Roles = currentUserRole,
                RoleList = roles
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(UserRoleViewModel model)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);

            bool roleExists = await this.roleManager.RoleExistsAsync(model.Role);

            if (!roleExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid Idenity details.");
            }

            var result = await this.userManager.RemoveFromRoleAsync(user, model.Role);

            if (result.Succeeded)
            {
                return this.RedirectToAction(nameof(Index), new { area = AreaConstants.Admin });
            }
            else
            {
                return this.View(model);
            }
        }

        public async Task<IActionResult> Role(string userId)
        {
            var user = await this.usersService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            var model = new ChangeUserRoleViewModel
            {
                User = user,
                Roles = roles
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(UserRoleViewModel model)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);

            bool roleExists = await this.roleManager.RoleExistsAsync(model.Role);

            if (!roleExists)
            {
                return this.View(model);
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            return this.RedirectToAction(nameof(Index), new { area = AreaConstants.Admin });
        }

        public async Task<IActionResult> ChangePassword(string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var model = new ChangePasswordViewModel
            {
                Username = user.UserName,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string userId, ChangePasswordViewModel model)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var code = await this.userManager.GeneratePasswordResetTokenAsync(user);

            var result = await this.userManager.ResetPasswordAsync(user, code, model.Password);

            if (result.Succeeded)
            {

                return this.RedirectToAction(nameof(Index), new { area = AreaConstants.Admin });
            }
            else
            {
                return this.View(model);
            }
        }

        public async Task<IActionResult> Delete(string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var model = new DeleteUserViewModel
            {
                UserId = user.Id,
                Username = user.UserName,
            };

            return this.View(model);
        }

        public async Task<IActionResult> DeleteUser(DeleteUserViewModel model, string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await this.userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return this.RedirectToAction(nameof(Index), new { area = AreaConstants.Admin });
            }
            else
            {
                return this.View(model);
            }
        }
    }
}