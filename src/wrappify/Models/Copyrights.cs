using Newtonsoft.Json;

namespace wrappify.Models
{
    [JsonObject]
    public class Copyrights
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}