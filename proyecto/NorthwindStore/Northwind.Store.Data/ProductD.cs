using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Store.Data
{
    public class ProductD : ICreateReadUpdateDelete<Model.Product>
    {
        public void Create(Product c)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product c)
        {
            throw new NotImplementedException();
        }

        public List<Product> Read()
        {
            throw new NotImplementedException();
        }

        public Product Read(int key)
        {
            throw new NotImplementedException();
        }

        public List<Product> Read(string filter)
        {
            throw new NotImplementedException();
        }

        public void Update(Product c)
        {
            throw new NotImplementedException();
        }

        public List<Product> Search(string filter, int page)
        {
            throw new NotImplementedException();
        }
    }
}
