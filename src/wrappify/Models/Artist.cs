using System.Collections.Generic;
using Newtonsoft.Json;

namespace wrappify.Models
{
    [JsonObject]
    public class Artist
    {
        [JsonProperty(PropertyName = "external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }
        
        [JsonProperty(PropertyName = "genres")]
        public string[] Genres { get; set; }
        
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "images")]
        public Image[] Images { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "popularity")]
        public int Popularity { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
    }
}