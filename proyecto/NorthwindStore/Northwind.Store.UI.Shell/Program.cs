using System;

namespace Northwind.Store.UI.Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Data.NWContext())
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
