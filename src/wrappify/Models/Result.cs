using System.Collections.Generic;
using Newtonsoft.Json;

namespace wrappify.Models
{
    [JsonObject]
    public class Paging<T>
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "items")]
        public List<T> Items { get; set; }

        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        [JsonProperty(PropertyName = "previous")]
        public string Previous { get; set; }

        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
    }
}
