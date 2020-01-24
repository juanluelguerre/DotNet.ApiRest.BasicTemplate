using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace ElGuerre.Items.Api
{
    [JsonObject("App")]
    public class AppSettings
    {
        public string DBConnectionString { get; set; }

    }
}
