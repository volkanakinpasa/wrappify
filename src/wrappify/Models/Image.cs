using Newtonsoft.Json;

namespace wrappify.Models
{
    [JsonObject]
    public class Image
    {
        [JsonProperty(PropertyName = "height")]
        public int? Height { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "width")]
        public int? Width { get; set; }

    }
}