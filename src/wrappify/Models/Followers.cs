using Newtonsoft.Json;

namespace wrappify.Models
{
    [JsonObject]
    public class Followers
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
    }
}