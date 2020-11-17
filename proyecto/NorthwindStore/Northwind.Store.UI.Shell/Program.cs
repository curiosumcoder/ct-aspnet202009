using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Northwind.Store.Data;

namespace Northwind.Store.UI.Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Uso de archivo de configuración
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<NWContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("NW"));
            #endregion

            //using (var db = new Data.NWContext())
            using (var db = new NWContext(optionsBuilder.Options))
            {
                foreach (var c in db.Customers)
                {
                    Console.WriteLine($"{c.CompanyName}");

                    foreach (var o in c.Orders)
                    {
                        Console.WriteLine($"{o.OrderId}");
                    }
                }
            }

            Console.WriteLine("READY!");
        }
    }
}
