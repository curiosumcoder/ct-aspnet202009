using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace Northwind.Store.UI.Intranet.Extensions
{
    /// <summary>
    /// http://devonburriss.me/asp-net-5-tips-tempdata
    /// Dado que TempData solamente acepta tipos string
    /// Para utilizar a TempData se debe habilitar Session
    /// services.AddSession();
    /// ...
    /// app.UseSession();
    /// </summary>
    public static class TempDataExtensions
    {
        public static void SetAsJson<T>(this ITempDataDictionary tempData, string key, T data)
        {
            var sData = JsonConvert.SerializeObject(data);
            tempData[key] = sData;
        }

        public static T GetFromJson<T>(this ITempDataDictionary tempData, string key)
        {
            if (tempData.ContainsKey(key))
            {
                var v = tempData[key];

                if (v is T)
                {
                    return (T)v;
                }

                if (v is string && typeof(T) != typeof(string))
                {
                    var obj = JsonConvert.DeserializeObject<T>((string)v);
                    return obj;
                }
            }
            return default(T);
        }
    }
}
