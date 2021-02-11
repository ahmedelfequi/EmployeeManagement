using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmployeeManagement
{
    public class Startup
    {

        private IConfiguration _config;

        // To use IConfiguration Service in Asp.Net Core
        public Startup(IConfiguration config)
        {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDBContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeConnection")));
            // EnableEndpointRouting = false in Asp.nect core 3.x version updates

            services.AddMvc(config => {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlSerializerFormatters();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

            /*
             AddIdentity() method adds the default identity system configuration for the specified user and role types.
IdentityUser class is provided by ASP.NET core and contains properties for UserName, PasswordHash, Email etc. This is the class that is used by default by the ASP.NET Core Identity framework to manage registered users of your application. 
If you want store additional information about the registered users like their Gender, City etc. Create a custom class that derives from IdentityUser. In this custom class add the additional properties you need and then plug-in this class instead of the built-in IdentityUser class. We will discuss how to do this in our upcoming videos.
Similarly, IdentityRole is also a builtin class provided by ASP.NET Core Identity and contains Role information.
We want to store and retrieve User and Role information of the registered users using EntityFrameWork Core from the underlying SQL Server database. We specify this using AddEntityFrameworkStores<AppDbContext>() passing our application DbContext class as the generic argument.
             */

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDBContext>();

            // To override Password options in IdetityOptions class

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireNonAlphanumeric = false;
            });
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
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes => {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }
}
