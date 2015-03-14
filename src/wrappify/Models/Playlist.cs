using System.Collections.Generic;
using Newtonsoft.Json;

namespace wrappify.Models
{
    [JsonObject]
    public class Playlist
    {
        [JsonProperty(PropertyName = "collaborative")] 
        public bool Collaborative { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }

        [JsonProperty(PropertyName = "followers")]
        public Followers Followers { get; set; }

        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "images")]
        public Image[] Images { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public User Owner { get; set; }

        [JsonProperty(PropertyName = "public")]
        public bool? Public { get; set; }

        [JsonProperty(PropertyName = "snapshot_id")]
        public string SnapshotId { get; set; }

        [JsonProperty(PropertyName = "tracks")]
        public Paging<playlisttrack> Tracks { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
    }
}
