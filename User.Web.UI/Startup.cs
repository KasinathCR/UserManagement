namespace User.Web
{
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using User.Dependencies;
    using User.Models;

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddMvc(
                options =>
                    {
                        options.EnableEndpointRouting = false;
                    })
                .AddRazorPagesOptions(
                    options =>
                        {
                            options.Conventions.AuthorizePage("/Account/NewUserVerfication");
                        });

            services.Configure<AppSettings>(this.Configuration.GetSection("AppSettings"));

            SimpleFactory.ResolveUiDependencies(services, this.Configuration);

            SimpleFactory.ResolveApiDependencies(services, this.Configuration);

            //services.AddControllersWithViews().AddRazorPagesOptions(
            //    options =>
            //        {
            //            options.Conventions.AuthorizePage("/Account/NewUserVerification");
            //        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
