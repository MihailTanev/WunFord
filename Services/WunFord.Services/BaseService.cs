namespace WunFord.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using WunFord.Data;
    using WunFord.Data.Models;

    public abstract class BaseService
    {
        protected BaseService(WunFordDbContext context, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.Context = context;
            this.Mapper = mapper;
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public WunFordDbContext Context { get; set; }

        public IMapper Mapper { get; set; }

        public UserManager<User> UserManager { get; set; }

        public SignInManager<User> SignInManager { get; set; }
    }
}
