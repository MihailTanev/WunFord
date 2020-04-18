using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WunFord.Common;
using WunFord.Data.Models;

namespace WunFord.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = UserConstants.EMAIL)]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = UserConstants.FIRST_NAME)]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = UserConstants.LAST_NAME)]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [StringLength(20, ErrorMessage = UserConstants.PASSWORD_ERROR_MESSAGE, MinimumLength = 6)]
            [Display(Name = UserConstants.PASSWORD)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = UserConstants.CONFIRM_PASSWORD)]
            [Compare(UserConstants.COMPARE_PASSWORD, ErrorMessage = UserConstants.PASSWORD_DO_NOT_MATCH)]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = this.Input.FirstName,
                    LastName = this.Input.LastName,
                };
                
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    if (this._userManager.Users.Count() == 1)
                    {
                        await this._userManager.AddToRoleAsync(user, RoleConstants.ADMIN_ROLE);
                    }
                    else
                    {
                        await this._userManager.AddToRoleAsync(user, RoleConstants.SPECIALIST_ROLE);
                    }

                    await this._signInManager.SignInAsync(user, false);
                }


                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
