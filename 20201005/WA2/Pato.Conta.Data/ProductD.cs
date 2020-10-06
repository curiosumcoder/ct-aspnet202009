using System;
using System.Collections.Generic;
using Pato.Conta.Model;

namespace Pato.Conta.Data
{
    public class ProductD
    {
        /// <summary>
        /// Se obtiene una lista de productos.
        /// </summary>
        /// <returns></returns>
        public List<Product> GetList()
        {
            var p = new Product();
            p.Id = 1;
            p.Nombre = "Leche en caja";

            return new List<Product>() {
                new Product() {
                    Id = 1, Nombre ="Leche en caja", Precio = 100
                },
                new Product() {
                    Id = 2, Nombre = "Galletas dulces", Precio = 500
                }
            };
        }
    }
}
