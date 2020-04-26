namespace WunFord.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WunFord.Data.ViewModels.User;

    public interface IUsersService
    {
        IEnumerable<UserViewModel> GetAllUsers();

        Task<UserViewModel> GetUserById(string userId);

    }
}
