using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publi4.Domain;
using Publi4.Domain.Entities;
using Publi4.Domain.Repositories;
using Publi4.Domain.Repositories.Interfaces;
using Publi4.Services;

namespace Publi4
{
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
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;                
            });

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(1);                
            });

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/LogIn");

            services.AddDbContext<Publi4DbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("Publi4DbContextConnection")));

            services.AddIdentity<Publi4User, Publi4Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 6;

            })
                .AddEntityFrameworkStores<Publi4DbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<Publi4User, Publi4Role, Publi4DbContext, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityUserToken<Guid>, IdentityRoleClaim<Guid>>>()
                .AddRoleStore<RoleStore<Publi4Role, Publi4DbContext, Guid, UserRole, IdentityRoleClaim<Guid>>>();



            services.AddRazorPages()
                    .AddRazorRuntimeCompilation();
            services.AddMvc();
            



            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            // Add application services.
#if DEBUG
            {
                services.AddTransient<IEmailSender, AuthMessageSenderDummy>();
            }
#else
            {
                //services.AddTransient<IEmailSender, EmailMessageSender>();
                //services.AddTransient<ISmsSender, EmailMessageSender>();
        }
#endif

            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
