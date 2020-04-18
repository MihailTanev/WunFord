namespace WunFord
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using WunFord.Data;
    using WunFord.Data.Models;
    using System.Linq;
    using WunFord.Services;
    using WunFord.Services.Interfaces;
    using global::AutoMapper;
    using WunFord.AutoMapper;
    using System.Globalization;
    using WunFord.Middleware.Extensions;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Configure Urls to lowercase
            services.AddRouting(routing => routing.LowercaseUrls = true);

            // Configure DbContext
            services.AddDbContext<WunFordDbContext>(options =>
                      options.UseLazyLoadingProxies()
                          .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            // Configure Identity
            services.AddIdentity<User, IdentityRole>()
                 .AddDefaultUI()
                 .AddDefaultTokenProviders()
                 .AddEntityFrameworkStores<WunFordDbContext>();

            // Configure Services
            services.AddTransient<IStatusesService, StatusesService>();
            services.AddTransient<ITicketsService, TicketsService>();
            services.AddTransient<IUsersService, UsersService>();

            // Configure password requirements
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 5;
            });

            // Configure AutoMapper
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MapperProfile()));
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Configure global Antiforgery
            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {               
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Middleware for adding roles in the database
            app.UseSeedRoles();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "areas",
                   template: "{area:exists}/{controller=Ticket}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Ticket}/{action=Index}/{id?}");
            });
        }
    }
}
