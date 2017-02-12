using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using TheGodfatherGM.Web.Models;

namespace TheGodfatherGM.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var connectionStringBuilder = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder
            {
                Server = "localhost",
                UserID = "godfather",
                Password = "",
                Database = "godfather",
                ConvertZeroDateTime = true
            };

            DefaultDbContext.ConnectionString = connectionStringBuilder.ToString();
            services.AddDbContext<DefaultDbContext>(options => options.UseMySql(DefaultDbContext.ConnectionString));


            services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<DefaultDbContext>()
                .AddDefaultTokenProviders();

            services
                .Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;

                    // Cookie settings
                    options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(180);
                    options.Cookies.ApplicationCookie.LoginPath = "/Account/Login";
                    options.Cookies.ApplicationCookie.LogoutPath = "/Account/Logout";
                    options.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
                    options.Cookies.ApplicationCookie.AutomaticChallenge = true;

                    // User settings
                    options.User.RequireUniqueEmail = true;
                });
            // services.AddTransient<IPasswordHasher<Account>, BCryptPasswordHasher>();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DefaultDbContext dbContext)
        {
            //dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            //dbContext.Database.Migrate();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseIdentity();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /*
         * public class BCryptPasswordHasher : IPasswordHasher<Account>
        {
            public string HashPassword(Accounts user, string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password);
            }

            public PasswordVerificationResult VerifyHashedPassword(Accounts user, string hashedPassword, string providedPassword)
            {
                if (BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword))
                    return PasswordVerificationResult.Success;

                return PasswordVerificationResult.Failed;
            }
        }
        */
    }
}
