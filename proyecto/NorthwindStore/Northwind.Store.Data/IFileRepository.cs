using System.IO;
using System.Threading.Tasks;

namespace Northwind.Store.Data
{
    /// <summary>
    /// Métodos generales para la lectura de archivos.
    /// </summary>
    /// <typeparam name="K"></typeparam>
    public interface IFileRepository<K>
    {
        Task<MemoryStream> GetFileStream(K id);

        Task<string> GetFileBase64(K id);
    }
}
