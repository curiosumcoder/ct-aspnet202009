using System;
using System.Collections.Generic;
using Northwind.Store.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Northwind.Store.Data
{
    public class CategoryRepository : BaseRepository<Category, int>, IFileRepository<int>
    {
        public CategoryRepository(NWContext context) : base(context) { }

        public override async Task<Category> Get(int key)
        {
            var result = await base.Get(key);

            result.PictureBase64 = await GetFileBase64(key);

            return result;
        }

        public override async Task<int> Delete(int key)
        {
            return await _db.Database.ExecuteSqlInterpolatedAsync(
                $"delete from categories where categoryid = {key}");
        }

        public async Task<IEnumerable<Category>> Search(string filter, PageFilter pf)
        {
            var result = new List<Category>();

            pf.Count = await _db.Categories.Where(c => string.IsNullOrEmpty(filter) || c.CategoryName.Contains(filter)).CountAsync();

            result = await _db.Categories.AsNoTracking().
                Where(c => string.IsNullOrEmpty(filter) || c.CategoryName.Contains(filter)).
                OrderBy(pf.Sorting).
                Skip((pf.Page - 1) * pf.PageSize).
                Take(pf.PageSize).ToListAsync();

            return result;
        }

        /// <summary>
        /// Lee la imagen de base de datos como un MemoryStream.
        /// </summary>
        /// <example>
        /// Para utilizarse en una acción de un Controller de ASP.NET MVC
        /// public FileStreamResult ReadImage(int id)
        /// {
        ///    return File(pB.ReadImageStream(id), "image/jpg");
        /// } 
        /// </example>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MemoryStream> GetFileStream(int id)
        {
            MemoryStream result = null;

            var image = await _db.Categories.Where(c => c.CategoryId == id).
                Select(i => i.Picture).AsNoTracking().FirstOrDefaultAsync();

            if (image != null)
            {
                result = new MemoryStream(image);
            }

            return result;
        }

        /// <summary>
        /// Lee la imagen de base de datos como un string en Base64.
        /// </summary>
        /// <example>
        /// Para utilizarse directamente en una vista de razor. 
        /// </example>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetFileBase64(int id)
        {
            string result = "";

            using (var ms = await GetFileStream(id))
            {
                if (ms != null)
                {
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    result = $"data:image/jpg;base64,{base64}";
                }
            }

            return result;
        }
    }
}
