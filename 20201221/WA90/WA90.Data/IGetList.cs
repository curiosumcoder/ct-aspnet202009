using System.Collections.Generic;

namespace WA90.Data
{
    public interface IGetList<T>
    {
        IEnumerable<T> GetList();
    }
}
