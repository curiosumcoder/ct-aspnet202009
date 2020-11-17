using System;
using System.Collections.Generic;
using Northwind.Store.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Northwind.Store.Data
{
    public class CategoryRepository : BaseRepository<Category, int>
    {
        public CategoryRepository(NWContext context) : base(context) { }

        public override async Task<IEnumerable<Category>> GetList(PageFilter pf = null)
        {
            return await _db.Categories.AsNoTracking().ToListAsync();
        }

        public override async Task<int> Delete(int key)
        {
            return await _db.Database.ExecuteSqlInterpolatedAsync(
                $"delete from categories where categoryid = {key}");
        }
    }
}
