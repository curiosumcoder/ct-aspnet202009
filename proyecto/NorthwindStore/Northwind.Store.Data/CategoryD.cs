using Northwind.Store.Model;
using System.Collections.Generic;
using System.Linq;

namespace Northwind.Store.Data
{
    public class CategoryD : ICreateReadUpdateDelete<Category>
    {
        readonly protected NWContext _db = null;

        public CategoryD(NWContext db)
        {
            _db = db;
        }        

        /// <summary>
        /// Método para crear una categoría
        /// </summary>
        /// <param name="c">Instancia de categoría.</param>
        public void Create(Category c)
        {           
            _db.Categories.Add(c);
            _db.SaveChanges();
        }

        public List<Category> Read()
        {
            return _db.Categories.ToList();
        }

        public Category Read(int key)
        {
            return _db.Categories.Find(key);
        }
        public List<Category> Read(string filter)
        {
            return _db.Categories.Where(c => c.CategoryName.Contains(filter)).ToList();
        }

        public void Update(Category c)
        {
            _db.Update(c);
            _db.SaveChanges();
        }

        public void Delete(Category c)
        {
            _db.Remove(c);
            _db.SaveChanges();
        }
    }
}
