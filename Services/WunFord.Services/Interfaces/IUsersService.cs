namespace WunFord.Services.Interfaces
{
    using System.Collections.Generic;
    using WunFord.Data.ViewModels.User;

    public interface IUsersService
    {
        IEnumerable<UserViewModel> GetAllUsers();

        UserViewModel GetUserById(string userId);

    }
}
