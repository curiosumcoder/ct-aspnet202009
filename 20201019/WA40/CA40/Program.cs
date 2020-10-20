using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Transactions;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace CA40
{
    class Program
    {
        /// <summary>
        /// https://github.com/dotnet/EntityFramework.Docs/tree/master/samples/core
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //LeerConfiguracion();
            //Basic();
            //CRUD();
            //LINQToEntities();
            //EagerLoading();
            LazyLoading();
            //LINQDynamic();
            //LINQToXML();
            //PLINQ();
            //Transactions();
            //RawSQL();
            //RawSQLClient();

            Console.WriteLine("READY");
            Console.ReadKey();
        }

        #region Demostraciones
        static string LeerConfiguracion()
        {
            // Soporte de archivo de configuración local
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var connStr = configuration.GetConnectionString("NW");

            Console.WriteLine(connStr);
            return connStr;
        }

        private static void Basic()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<NWContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("NW"));

            //using (var db = new NWContext())
            using (var db = new NWContext(optionsBuilder.Options))
            {
                // query
                var q0 = from p in db.Products
                         where p.ProductName.Contains("queso")
                         select p;

                var q1 = (from p in db.Products
                          where p.ProductName.Contains("queso")
                          select p).All(p => p.Discontinued);

                // métodos de extensión + expresiones lambda
                var query = db.Products.Where(p => p.ProductName.Contains("queso"));

                bool discontinued = db.Products.All(p => p.Discontinued);

                //foreach (var p in db.Products)
                foreach (var p in query)
                {
                    Console.WriteLine($"{p.ProductId} {p.ProductName}");
                }
            }
        }

        private static void CRUD()
        {
            using (var db = new NWContext())
            {
                int lastId = db.Products.Max(p => p.ProductId);

                var np = new Product()
                {
                    ProductName = $"Demostración #{++lastId}",
                    Discontinued = false
                };

                db.Products.Add(np);
                db.SaveChanges();

                np.ProductName += " ACTUALIZADO";

                foreach (var e in db.ChangeTracker.Entries<Product>())
                {
                    Console.WriteLine($"{e.Property("ProductName").CurrentValue} {e.State}");
                }

                db.SaveChanges();

                db.Products.Remove(np);
                db.SaveChanges();

                foreach (var p in db.Products.Include(p => p.Category))
                {
                    Console.WriteLine($"{p.ProductId}, {p.ProductName}, {p.UnitPrice}, {p.Category?.CategoryName ?? "Sin categoría"}");
                }

                Console.WriteLine($"Total de productos: {db.Products.Count()}");

                Console.ReadLine();
            }
        }

        static void LINQToEntities()
        {
            using (NWContext db = new NWContext())
            {
                // Filtro
                var q1 = from c in db.Customers
                         where c.Country == "Mexico"
                         select c;

                var q1x = db.Customers.
                    Where(c => c.Country == "Mexico");

                //var result = q1.Count();
                //var any = result.Any();

                foreach (var item in q1x)
                {
                    Console.WriteLine($"{item.CustomerId} {item.CompanyName} {item.ContactName} {item.Country}");
                }
            }

            // Proyecciones
            using (NWContext db = new NWContext())
            {
                var q2 = from c in db.Customers
                         select c.Country;
                var q2x = db.Customers.Select(c => c.Country);

                var q2y = from c in db.Customers
                          select new { c.CustomerId, c.ContactName };

                var q2z = db.Customers.Select(c =>
                    new { Id = c.CustomerId, c.ContactName });

                var q2w = db.Customers.Select(c =>
                    new Categories() { CategoryName = c.ContactName });

                Console.Clear();
                foreach (var item in q2z)
                {
                    Console.WriteLine($"{item.Id}, {item.ContactName}");
                }
            }

            // SelectMany
            using (NWContext db = new NWContext())
            {
                var q4 = db.Customers.
                    Where(c => c.Country == "Mexico").
                    SelectMany(c => c.Orders);

                var q4x = db.Orders.
                    Where(o => o.Customer.Country == "Mexico");

                Console.Clear();
                foreach (var item in q4)
                {
                    Console.WriteLine($"{item.CustomerId}, {item.OrderId}");
                }
            }

            // Ordenamiento
            using (NWContext db = new NWContext())
            {
                var q5 = from c in db.Customers
                         where c.Orders.Count > 5
                         orderby c.Country descending
                         select c;

                var q5x = db.Customers.
                    Where(c => c.Orders.Count > 5).
                    OrderByDescending(c => c.Country);

                Console.Clear();
                foreach (var item in q5)
                {
                    Console.WriteLine($"{item.CompanyName}, {item.Country}");
                }

                var q6 = from c in db.Customers
                         orderby c.CompanyName, c.ContactTitle,
                         c.ContactName
                         select c;

                var q6x = db.Customers.OrderBy(c =>
                        new
                        {
                            c.CompanyName,
                            c.ContactTitle
                        }).
                    ThenBy(c => c.ContactName);

                Console.Clear();
                foreach (var item in q6)
                {
                    Console.WriteLine($"{item.CompanyName}, {item.Country}");
                }
            }

            // Agrupamiento
            using (NWContext db = new NWContext())
            {
                var q7 = from c in db.Customers
                         group c by c.Country into CustByCountry
                         select CustByCountry;

                var q7x = db.Customers.GroupBy(c => c.Country);

                Console.Clear();
                foreach (var item in q7)
                {
                    Console.WriteLine($"{item.Key}, {item.Count()}");

                    foreach (var c in item)
                    {
                        Console.WriteLine($"\t{c.ContactName}");
                    }
                }

                var q7y = from c in db.Customers
                          group c by new { c.Country, c.City } into CountryCity
                          where CountryCity.Count() > 1
                          select new
                          {
                              Country = CountryCity.Key.Country,
                              City = CountryCity.Key.City,
                              Count = CountryCity.Count(),
                              Items = CountryCity
                          };

                var q7y2 = db.Customers.GroupBy(c => new { c.Country, c.City }).
                    Where(g => g.Count() > 1).
                    Select(g => new
                    {
                        Country = g.Key.Country,
                        City = g.Key.City,
                        Count = g.Count(),
                        Items = g
                    });

                Console.Clear();
                foreach (var item in q7y)
                {
                    Console.WriteLine($"{item.Country}, {item.City}, {item.Count}");

                    foreach (var c in item.Items)
                    {
                        Console.WriteLine($"\t{c.ContactName}");
                    }
                }
            }

            // Join
            using (NWContext db = new NWContext())
            {
                var q8 = from c in db.Customers
                         join o in db.Orders on c.CustomerId
                         equals o.CustomerId
                         select new { c, o };

                //                new { c.CustomerID, c.Country }
                //equals new { o.CustomerID, Country =  o.ShipCountry }

                var q8x = db.Customers.Join(
                    db.Orders, c => c.CustomerId,
                    o => o.CustomerId,
                    (c, o) => new { c, o });

                Console.Clear();
                foreach (var item in q8)
                {
                    Console.WriteLine($"{item.c.CustomerId}, {item.o.OrderId}");
                }

                // Join agrupado
                var q8y = from c in db.Customers
                          join o in db.Orders on c.CustomerId
                          equals o.CustomerId into CustomerOrders
                          select new { c, Orders = CustomerOrders };
                //select CustomerOrders;

                foreach (var ordenes in q8y)
                {
                    //foreach (var orden in ordenes)
                    //{

                    //}
                }

                // Left Ourter Join
                var q8z = from c in db.Customers
                          join o in db.Orders on c.CustomerId
                          equals o.CustomerId into CustomerOrders
                          from detalle in CustomerOrders.DefaultIfEmpty()
                          select new
                          {
                              Customer = c,
                              Order = detalle
                          };

                foreach (var item in q8z)
                {
                    if (item.Order == null)
                    {
                        Console.WriteLine($"Customer {item.Customer.CustomerId} with NO orders!");
                    }
                }
            }

            // Conjuntos
            using (NWContext db = new NWContext())
            {
                var q9 = db.Customers.
                    Select(c => c.Country).Distinct();

                var q10 = db.Customers.Except(
                    db.Customers.Where(
                    c => c.Country == "Mexico")).
                    Select(c => c.Country).Distinct();

                Console.Clear();
                foreach (var item in q10)
                {
                    Console.WriteLine($"{item}");
                }
            }

            // Partición (paginación)
            using (NWContext db = new NWContext())
            {
                var q11 = db.Customers.
                    OrderBy(c => c.CustomerId).
                    Skip(10);
                // Tomar los primero 10 elementos
                var q12 = db.Customers.
                    OrderBy(c => c.CustomerId).
                    Take(10);
                // Segunda página de 10 elementos
                var q13 = db.Customers.
                    OrderBy(c => c.CustomerId).
                    Skip(10).Take(10);

                Console.Clear();
                foreach (var item in q13)
                {
                    Console.WriteLine($"{item.CustomerId}, {item.CompanyName}");
                }
            }

            // Modificación de consulta
            using (NWContext db = new NWContext())
            {
                var q14 = db.Customers.
                    Where(c => c.Orders.Count > 5);

                Console.Clear();
                Console.WriteLine(q14.Count());

                q14 = q14.Where(c => c.Country == "Mexico");
                Console.WriteLine(q14.Count());

                q14 = q14.OrderByDescending(c => c.ContactName);

                foreach (var item in q14)
                {
                    Console.WriteLine(item.ContactName);
                }
            }

            // Métodos útiles
            using (NWContext db = new NWContext())
            {
                var o1 = db.Customers.First();
                o1 = db.Customers.FirstOrDefault();
                o1 = db.Customers.Where(c => c.CustomerId == "ALFKI")
                    .Single();
                o1 = db.Customers.Where(c => c.CustomerId == "ALFKI").
                    SingleOrDefault();

                var o2 = db.Customers.All(c => c.Orders.Count > 5 &&
                        c.Country == "Mexico");
                o2 = db.Customers.
                    Any(c => c.Orders.Count > 5);

                var sum = db.OrderDetails.
                    Sum(od => od.Quantity * od.UnitPrice);
            }
        }

        static void EagerLoading()
        {
            // Eager Loading
            using (NWContext db = new NWContext())
            {
                // Proyección
                var customersOrders =
                    from c in db.Customers.
                        OrderBy(c => c.CustomerId).
                        Skip(10).Take(2)
                    select new
                    {
                        Cliente = c,
                        Ordenes = c.Orders
                    };

                foreach (var c in customersOrders)
                {
                    Console.WriteLine($"{c.Cliente.CustomerId}, {c.Cliente.ContactName}");
                    foreach (var o in c.Ordenes)
                    {
                        Console.WriteLine($"{o.OrderId}, {o.OrderDate}");
                    }
                }

                var customersOrders2 = from c in db.Customers.
                       Include(c => c.Orders).
                       Include("Orders.OrderDetails").
                       OrderBy(c => c.CustomerId).
                       Skip(10).Take(2)
                                       select c;

                var customersOrders2x = db.Customers.
                    Include("Orders").
                    Include("CustomerDemographics").Take(2);

                foreach (var c in customersOrders2)
                {
                    Console.WriteLine($"{c.CustomerId}, {c.ContactName}");
                    foreach (var o in c.Orders)
                    {
                        Console.WriteLine($"{o.OrderId}, {o.OrderDate}");

                        foreach (var od in o.OrderDetails)
                        {
                            Console.WriteLine($"{od.ProductId}, {od.Quantity}");
                        }
                    }
                }
            }
        }

        static void LazyLoading()
        {
            // Lazy Loading
            using (NWContext db = new NWContext())
            {
                //bool isLazy = true;
                //db.ChangeTracker.LazyLoadingEnabled = isLazy; // default = true

                // .Include(c=>c.Orders). // Eager Loading
                var customers = db.Customers.
                    OrderBy(c => c.CustomerId);

                foreach (var c in customers.ToList())
                {
                    Console.WriteLine($"{c.CustomerId}, {c.ContactName}");

                    //if (!isLazy)
                    //{
                    //    db.Entry(c).Collection(o => o.Orders).Load();
                    //}

                    Console.WriteLine($"Número de Órdenes: {c.Orders.Count}");
                }

                foreach (var o in db.Orders)
                {
                    //if (!isLazy)
                    //{
                    //    db.Entry(o).Reference(c => c.Customer).Load();
                    //}

                    Console.WriteLine(o.Customer.ContactName);
                    Console.WriteLine($"{o.OrderId}, {o.OrderDate}");
                }
            }
        }

        // https://dynamic-linq.net/
        static void LINQDynamic()
        {
            // Ordenamiento
            using (NWContext db = new NWContext())
            {
                string order = "Country DESC, City";

                var q1 = db.Customers.Where(c => c.Orders.Count > 5).OrderBy(order);

                Console.Clear();
                foreach (var item in q1)
                {
                    Console.WriteLine($"{item.CompanyName.PadRight(35)}, {item.Country}, {item.City}");
                }
            }
        }

        static void LINQToXML()
        {
            using (NWContext db = new NWContext())
            {
                List<Customers> customers = db.Customers.ToList();

                // LINQ to Objects
                var q1 = from c in customers
                         where c.Country == "Mexico"
                         select c;
                var q1x = customers.Where(c => c.Country == "Mexico");

                // LINQ to XML
                var docXML = new XElement("customers",
                    from c in customers
                    select new XElement("customer",
                        new XElement("id", c.CustomerId),
                        new XElement("companyName", c.CompanyName),
                        new XElement("contactName", c.ContactName))
                    );
                docXML.Save("customer.xml");

                var docXML2 = XElement.Load("customer.xml");
                var query = from c in docXML2.Descendants("customer")
                            where c.Element("companyName").Value.StartsWith("A", StringComparison.CurrentCulture)
                            select new Customers()
                            {
                                CustomerId = c.Element("id").Value,
                                CompanyName = c.Element("companyName").Value,
                                ContactName = c.Element("contactName").Value
                            };

                foreach (var item in query)
                {
                    Console.WriteLine(item.CompanyName);
                }
            }
        }

        static void PLINQ()
        {
            Console.WriteLine("Experimento PLINQ en proceso ...");

            // Parallel LINQ
            var nums = Enumerable.Range(1, 10000);

            var query = from n in nums.AsParallel()
                        where ToDo(n) == n
                        select ToDo(n);

            var sw = System.Diagnostics.Stopwatch.StartNew();

            var result = query.ToList();

            sw.Stop();

            Console.WriteLine($"Duración: {sw.ElapsedMilliseconds}");
        }

        static int ToDo(int n)
        {
            System.Threading.Thread.SpinWait(1000);
            return n;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/ef/core/saving/transactions
        /// </summary>
        static void Transactions()
        {
            using (var db = new NWContext())
            {
                var tran = db.Database.BeginTransaction();
                try
                {
                    // Acciones
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }

            using (var ts = new TransactionScope())
            {
                using (var db = new NWContext())
                {
                    // CRUD actions
                    db.SaveChanges();
                }
                ts.Complete();
            }
        }

        static void RawSQL()
        {
            using (var db = new NWContext())
            {
                // https://docs.microsoft.com/en-us/ef/core/querying/raw-sql
                // Prior to version 3.0, FromSqlRaw and FromSqlInterpolated were two overloads named FromSql
                //var q2 = db.Products.FromSql("",null)

                var quantity = 12;
                int productsTotal = db.Database.ExecuteSqlInterpolated($"update [Order Details] set Quantity = {quantity} where OrderID = 10248 and ProductID = 11");

                var pQuantity = new SqlParameter("quantity", quantity);
                int affected = db.Database.ExecuteSqlRaw("update [Order Details] set Quantity = @quantity where OrderID = 10248 and ProductID = 11", pQuantity);

                var filter = 1;
                var products0 = db.Products.FromSqlInterpolated(@$"SELECT [ProductID],[ProductName],[SupplierID],[CategoryID]
                        ,[QuantityPerUnit],[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel],[Discontinued]
                        FROM[dbo].[Products] WHERE [ProductID] = {filter}");

                var pFilter = new SqlParameter("filter", filter);
                var products1 = db.Products.FromSqlRaw(@$"SELECT [ProductID],[ProductName],[SupplierID],[CategoryID]
                        ,[QuantityPerUnit],[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel],[Discontinued]
                        FROM[dbo].[Products] WHERE [ProductID] = @filter", pFilter);
            }
        }

        /// <summary>
        /// https://www.nuget.org/packages/Microsoft.Data.SqlClient/
        /// https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-code-examples#sqlclient
        /// </summary>
        static void RawSQLClient()
        {
            var connStr = LeerConfiguracion();
            var customerId = "VINET";

            using (var conn = new SqlConnection(connStr))
            {
                var comm = conn.CreateCommand();
                comm.CommandText = @"select o.*, 
                    (select sum(od.Quantity * od.UnitPrice)
                    from[Order Details] od
                    where od.OrderID = o.OrderID) OrderTotal
                from Orders o
                where o.CustomerID = @customerId";
                comm.Parameters.Add(new SqlParameter("customerId", customerId));

                conn.Open();

                using (var reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var oId = reader.GetInt32(0);
                        var oTotal = Convert.ToDecimal(reader["OrderTotal"]);
                    }
                }

                var da = new SqlDataAdapter(comm);
                var dt = new DataTable();
                da.Fill(dt);

                // Se puede usar LINQ to DataSets
                var columns = dt.Columns;
                foreach (var dr in dt.AsEnumerable())
                {
                    var line = new StringBuilder();
                    foreach (DataColumn c in columns)
                    {
                        line.Append($"{dr.Field<int>("OrderID")}, ");
                    }
                    Console.WriteLine(line);
                }
            }
        }
        #endregion
    }
}
