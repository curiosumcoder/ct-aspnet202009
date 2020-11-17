using Northwind.Store.Notification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Northwind.Store.Data
{
    /// <summary>
    /// CRUD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TK"></typeparam>
    public interface IRepository<T, TK> : IDisposable
    {
        Task<int> Save(T model, Notifications n = null);
        Task<T> Get(TK key);
        Task<IEnumerable<T>> GetList(PageFilter pf = null);
        Task<IEnumerable<T>> Find(Expression<Func<T,bool>> predicate);
        Task<int> Delete(TK key);
    }
}
