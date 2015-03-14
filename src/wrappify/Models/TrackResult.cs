using Newtonsoft.Json;

namespace wrappify.Models
{
    public class TrackResult
    {
        [JsonProperty(PropertyName = "tracks")]
        public Track[] Tracks { get; set; }  
    }
}