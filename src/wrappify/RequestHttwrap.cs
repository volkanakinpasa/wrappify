using System.Threading.Tasks;
using Httwrap;
using Httwrap.Interface;
using wrappify.Responses;

namespace wrappify
{
    public class RequestHttwrap : RequestStrategy
    {
        private readonly string _baseUrl;

        public RequestHttwrap(RequestConfiguration requestConfiguration)
        {
            _baseUrl = Helper.BuildUrl(requestConfiguration);
        }

        public async Task<SpotifyResponse> GetAsync(string path)
        {
            IHttwrapConfiguration configuration = new HttwrapConfiguration(_baseUrl);

            HttwrapClient client = new HttwrapClient(configuration);

            IHttwrapResponse httwrapResponse = await client.GetAsync(path);

            SpotifyResponse response = new SpotifyResponse(httwrapResponse.Body, httwrapResponse.StatusCode);

            return response;
        }
    }
}