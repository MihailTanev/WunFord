namespace WunFord.Common
{
    public static class UserConstants
    {
        public const string FIRST_NAME = "First Name";
        public const string LAST_NAME = "Last Name";

        public const string PASSWORD_ERROR_MESSAGE = "The {0} must be at least {2} and at max {1} characters long.";
        public const string COMPARE_PASSWORD = "Password";

        public const string CONFIRM_PASSWORD = "Confirm Password";
        public const string PASSWORD_DO_NOT_MATCH = "The password and confirmation password do not match.";


        public const string EMAIL = "Email";
        public const string PASSWORD = "Password";


        public const int MIN_USERNAME_LENGTH = 3;
        public const int MAX_USERNAME_LENGTH = 15;

        public const int MIN_BIRTHDATE_YEAR = 1900;
        public const int MAX_BIRTHDATE_YEAR = 2019;

        public const string REGEX_USERNAME = "[a-zA-z0-9-.*/_]+";

        public const string USERNAME_LENGHT_ERROR_MESSAGE = "The {0} must be at least {1} characters long!";

    }
}
