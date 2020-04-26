namespace WunFord.Data.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;
    using WunFord.Common;

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [DataType(DataType.Text)]
        [MinLength(UserConstants.MIN_USERNAME_LENGTH, ErrorMessage = UserConstants.USERNAME_LENGHT_ERROR_MESSAGE)]
        [MaxLength(UserConstants.MAX_USERNAME_LENGTH)]
        [RegularExpression(UserConstants.REGEX_USERNAME)]
        public string Username { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = UserConstants.FIRST_NAME)]
        public string FirstName { get; set; }


        [Display(Name = UserConstants.LAST_NAME)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [EmailAddress]
        [Display(Name = UserConstants.EMAIL)]
        public string Email { get; set; }
        
    }
}
