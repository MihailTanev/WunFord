namespace WunFord.Data.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;
    using WunFord.Common;

    public class ChangePasswordViewModel
    {
        public string Email { get; set; }

        public string Username { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = UserConstants.PASSWORD_ERROR_MESSAGE, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = UserConstants.PASSWORD)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = UserConstants.CONFIRM_PASSWORD)]
        [Compare(UserConstants.COMPARE_PASSWORD, ErrorMessage = UserConstants.PASSWORD_DO_NOT_MATCH)]
        public string ConfirmPassword { get; set; }
    }
}
