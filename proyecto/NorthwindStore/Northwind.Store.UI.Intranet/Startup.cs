using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Northwind.Store.UI.Intranet.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northwind.Store.Data;
using Northwind.Store.Model;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Northwind.Store.UI.Intranet.Authorization;

namespace Northwind.Store.UI.Intranet
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
            //services.AddDbContext<NWContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NW")));
            services.AddDbContextPool<NWContext>(options =>
            {
                options.UseLoggerFactory(LoggerFactory.Create(builder =>
                {
                    builder.AddConsole(); 
                    builder.AddDebug();
                }));
                options.UseSqlServer(Configuration.GetConnectionString("NW"));
            }); // 128

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("NW")));
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("NW")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();

            #region Requerir autenticación para todo el sitio
            // Requerir autenticación para todo el sitio, se exceptúa
            // el uso específico de Authorize o Allowanonymous
            //services.AddControllersWithViews(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //                     .RequireAuthenticatedUser()
            //                     .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});

            // Requerir autenticación para todo el sitio, se exceptúa
            // el uso específico de Authorize o AllowAnonymous. RECOMENDADO    
            //services.AddAuthorization(options =>
            //{
            //    options.FallbackPolicy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //});
            #endregion

            services.AddRazorPages();

            services.AddTransient<IRepository<Category, int>, BaseRepository<Category, int>>();
            services.AddTransient<IRepository<Category, int>, CategoryRepository>();
            services.AddTransient(typeof(CategoryRepository));
            services.AddTransient(typeof(CategoryD));

            // AuthorizationHandler
            services.AddSingleton<IAuthorizationHandler, AdministratorAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ManagerAuthorizationHandler>();
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

            //app.UseStatusCodePagesWithRedirects("/Home/ErrorWithCode?code={0}");
            //app.UseStatusCodePagesWithRedirects("/Error.html?code={0}");
            app.UseStatusCodePagesWithReExecute("/Home/ErrorWithCode", "?code={0}");

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute("admin", "admin", "admin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
