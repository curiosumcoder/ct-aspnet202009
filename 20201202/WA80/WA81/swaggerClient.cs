using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WA81
{
    public partial class swaggerClient
    {
        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings) 
        {
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.NullValueHandling = NullValueHandling.Ignore;
        }
    }
}
