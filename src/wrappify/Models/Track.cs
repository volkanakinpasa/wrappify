using System.Collections.Generic;
using Newtonsoft.Json;

namespace wrappify.Models
{
    [JsonObject]
    public class Track
    {
        [JsonProperty(PropertyName = "album")]
        public Album Album { get; set; }

        [JsonProperty(PropertyName = "artists")]
        public Artist[] Artists { get; set; }

        [JsonProperty(PropertyName = "available_markets")]
        public string[] AvailableMarkets { get; set; }

        [JsonProperty(PropertyName = "disc_number")]
        public int DiscNumber { get; set; }

        [JsonProperty(PropertyName = "duration_ms")]
        public int DurationMs { get; set; }

        [JsonProperty(PropertyName = "explicit")]
        public bool Explicit { get; set; }

        [JsonProperty(PropertyName = "external_ids")]
        public Dictionary<string, string> ExternalIds { get; set; }

        [JsonProperty(PropertyName = "external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }

        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "is_playable")]
        public bool IsPlayable { get; set; }

        [JsonProperty(PropertyName = "linked_from")]
        public TrackLink LinkedFrom { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "popularity")]
        public int Popularity { get; set; }

        [JsonProperty(PropertyName = "preview_url")]
        public string PreviewUrl { get; set; }

        [JsonProperty(PropertyName = "track_number")]
        public int TrackNumber { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
    }
}