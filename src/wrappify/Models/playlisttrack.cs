using Newtonsoft.Json;

namespace wrappify.Models
{
    [JsonObject]
    public class playlisttrack
    {
        public string added_at { get; set; }
        public User added_by { get; set; }
        public Track track { get; set; }
    }
}