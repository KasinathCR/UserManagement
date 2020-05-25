namespace User.Dependencies
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using User.Entities;
    using User.Web.API.Repository.Implementations;
    using User.Web.API.Repository.Interfaces;
    using User.Web.API.Services.Implementations;
    using User.Web.API.Services.Interfaces;
    using User.Web.UI.Services.Implementations;
    using User.Web.UI.Services.Interfaces;

    public static class SimpleFactory
    {
        public static void ResolveApiDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                optionsAction: options => options.UseSqlServer(
                    connectionString: configuration.GetConnectionString(name: "ApplicationDbContext")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IAccountApiService, AccountApiService>();
            services.AddTransient<IAccountRepository, AccountRepository>();
        }

        public static void ResolveUiDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAccountUiService, AccountUiService>();
        }
    }
}
