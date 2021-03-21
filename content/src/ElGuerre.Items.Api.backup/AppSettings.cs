using Newtonsoft.Json;

namespace ElGuerre.Items.Api
{
    [JsonObject("App")]
    public class AppSettings
    {
        public string DBConnectionString { get; set; }

    }
}
