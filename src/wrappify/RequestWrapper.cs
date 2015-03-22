using System.Net.Http;
using System.Threading.Tasks;
using wrappify.Interfaces;
using wrappify.Responses;

namespace wrappify
{
    public class RequestWrapper : RequestStrategy
    {
        private readonly string _baseUrl;

        public RequestWrapper(RequestConfiguration requestConfiguration)
        {
            _baseUrl = Helper.BuildUrl(requestConfiguration);
        }

        public async Task<SpotifyResponse> GetAsync(string path)
        {
            IRequestManager manager = new RequestManager();

            string url = string.Format("{0}/{1}", _baseUrl, path);

            HttpResponseMessage message = await manager.GetAsync(url);

            string responseJson = await message.Content.ReadAsStringAsync();

            SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

            return spotifyResponse;
        }
    }
}