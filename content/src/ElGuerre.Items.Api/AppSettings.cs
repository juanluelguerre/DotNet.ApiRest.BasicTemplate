using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElGuerre.Items.Api
{
    [JsonObject("App")]
    public class AppSettings
    {
        public string DBConnectionString { get; set; }

    }
}
