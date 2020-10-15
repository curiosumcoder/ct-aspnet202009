using System;
using System.Linq;

namespace CA40
{
    class Program
    {
        static void Main(string[] args)
        {            
            using (var db = new NWContext())
            {
                var query = db.Products.Where(p=> p.ProductName.Contains("queso"));

                //foreach (var p in db.Products)
                foreach (var p in query)
                {
                    Console.WriteLine($"{p.ProductId} {p.ProductName}");
                }
            }

            Console.WriteLine("READY");
            Console.ReadKey();
        }
    }
}
