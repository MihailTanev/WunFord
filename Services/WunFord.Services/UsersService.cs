﻿namespace WunFord.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using WunFord.Data;
    using WunFord.Data.Models;
    using WunFord.Data.ViewModels.User;
    using WunFord.Services.Interfaces;

    public class UsersService : BaseService, IUsersService
    {
        public UsersService(WunFordDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        : base(context, mapper, userManager, signInManager)
        {
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            var users = this.Context
                 .Users
                 .OrderBy(x => x.UserName)
                 .AsQueryable();

            var usersAdminViewModel = this.Mapper.Map<IQueryable<User>, IEnumerable<UserViewModel>>(users);

            return usersAdminViewModel;
        }

        public async Task<UserViewModel> GetUserById(string userId)
        {
            var user = await this.Context
                  .Users
                  .FirstOrDefaultAsync(d => d.Id == userId);

            var ticketViewModel = this.Mapper.Map<UserViewModel>(user);

            return ticketViewModel;
        }
    }
}
