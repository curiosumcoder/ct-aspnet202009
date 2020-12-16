using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Northwind.Store.UI.Intranet.Data;
using Northwind.Store.UI.Web.Data;

namespace Northwind.Store.UI.Intranet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                //CreateHostBuilder(args).Build().Run();
                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var hostingEnvironment = services.GetService<IWebHostEnvironment>();

                    if (!hostingEnvironment.IsProduction())
                    {
                        try
                        {
                            var context = services.GetRequiredService<ApplicationDbContext>();
                            context.Database.Migrate();

                            SeedData.Initialize(services).Wait();
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "An error occurred seeding the DB.");
                        }
                    }
                }

                host.Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

            //CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseNLog();  // NLog: Setup NLog for Dependency injection;
    }
}
