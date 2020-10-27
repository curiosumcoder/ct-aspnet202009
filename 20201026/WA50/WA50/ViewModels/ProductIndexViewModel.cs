using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WA50.ViewModels
{
    public class ProductIndexViewModel
    {
        public string Filter { get; set; }
        public List<Product> Products { get; set; }

        public int TotalCount { get; set; }
        public int PageSize => 5;
        public int PageCount => TotalCount / PageSize;

        /// <summary>
        /// Página actual
        /// </summary>
        public int Page { get; set; }
    }
}
