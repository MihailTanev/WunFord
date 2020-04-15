using System;

namespace WunFord.Data.ViewModels.User
{
    public class UserViewModel : IEquatable<UserViewModel>
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool Equals(UserViewModel other)
        {
            return this.Id == other.Id && this.Username == other.Username && this.Email==other.Email;
        }
    }
}
