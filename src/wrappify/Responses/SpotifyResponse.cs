using System.Net;
using Newtonsoft.Json;

namespace wrappify.Responses
{
    public class SpotifyResponse
    {
        private readonly string _body;
        private readonly HttpStatusCode _statusCode;

        public SpotifyResponse(string body, HttpStatusCode statusCode)
        {
            _body = body;
            _statusCode = statusCode;
        }

        public T Result<T>()
        {
            T result = JsonConvert.DeserializeObject<T>(_body, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return result;
        }

        public HttpStatusCode StatusCode
        {
            get { return _statusCode; }
        }
    }
}