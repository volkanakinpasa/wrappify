using Newtonsoft.Json;

namespace wrappify.Models
{
    [JsonObject]
    public class ErrorModel
    {
        [JsonProperty(PropertyName = "error")]
        public Error Error { get; set; }
    }

    [JsonObject]
    public class Error
    {
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}