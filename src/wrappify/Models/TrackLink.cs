using System.Collections.Generic;
using Newtonsoft.Json;

namespace wrappify.Models
{
    [JsonObject]
    public class TrackLink
    {
        [JsonProperty(PropertyName = "external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }

        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
    }
}