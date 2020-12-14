using Northwind.Store.Data;
using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Store.UI.Intranet.ViewModels
{
    public class CategoryIndexViewModel
    {
        //IRepository<Category, int> _cR;
        CategoryRepository _cR;

        public string Command { get; set; } = "list";
        public Category Filter { get; set; } = new Category();
        public IEnumerable<Category> Items { get; set; } = new List<Category>();
        public PageFilter Paging { get; set; } = new PageFilter() { Sort = "CategoryName" };

        //public async Task HandleRequest(IRepository<Category, int> cR)
        public async Task HandleRequest(CategoryRepository cR)
        {
            _cR = cR;
            
            switch (Command)
            {
                case "list":
                    await list();
                    break;
                case "search":
                case "paging":
                case "order":
                    await search();
                    break;
                default:
                    break;
            }
        }

        async Task list()
        {
            Items = await _cR.GetList(Paging);
        }

        async Task search() 
        {
            Items = await _cR.Search(Filter.CategoryName, Paging);
        }
    }
}
