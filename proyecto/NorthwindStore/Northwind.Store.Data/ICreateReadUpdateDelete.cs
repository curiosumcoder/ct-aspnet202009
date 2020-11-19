using Northwind.Store.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Store.Data
{
    public interface ICreateReadUpdateDelete<T>
    {
        void Create(T c);

        List<T> Read();

        T Read(int key);

        List<T> Read(string filter);

        void Update(T c);

        void Delete(T c);
    }
}
