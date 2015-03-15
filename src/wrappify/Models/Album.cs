using System.Collections.Generic;
using Newtonsoft.Json;

namespace wrappify.Models
{
    [JsonObject]
    public class AlbumModel
    {
        [JsonProperty(PropertyName = "albums")]
        public Album[] Albums { get; set; }    
    }

    [JsonObject]
    public class Album
    {
        [JsonProperty(PropertyName = "album_type")]
        public string AlbumType { get; set; }

        [JsonProperty(PropertyName = "artists")]
        public Artist[] Artists { get; set; }

        [JsonProperty(PropertyName = "available_markets")]
        public string[] AvailableMarkets { get; set; }

        [JsonProperty(PropertyName = "copyrights")]
        public Copyrights[] Copyrights { get; set; }

        [JsonProperty(PropertyName = "external_ids")]
        public Dictionary<string, string> ExternalIds { get; set; }

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

        [JsonProperty(PropertyName = "release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty(PropertyName = "release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        [JsonProperty(PropertyName = "tracks")]
        public Paging<Track> Tracks { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
    }
}