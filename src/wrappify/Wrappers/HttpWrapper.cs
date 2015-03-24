using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using wrappify.Responses;

namespace wrappify.Wrappers
{
    public class HttpWrapper : IHttpWrapper
    {
        private readonly string _baseUrl;

        public HttpWrapper(RequestConfiguration requestConfiguration)
        {
            _baseUrl = Helper.BuildUrl(requestConfiguration);
        }

        public async Task<SpotifyResponse> GetAsync(string path)
        {
            string url = string.Format("{0}{1}", _baseUrl, path);
           
            HttpClient client = new HttpClient();

            HttpResponseMessage message = await client.GetAsync(url);

            string responseJson = await message.Content.ReadAsStringAsync();
            
            HandleIfAndError(message, responseJson);

            SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

            return spotifyResponse;
        }

        public async Task<SpotifyResponse> GetAsync(string path, string accessToken, bool bearer)
        {
            string url = string.Format("{0}{1}", _baseUrl, path);

            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpResponseMessage message = await client.GetAsync(url);

            string responseJson = await message.Content.ReadAsStringAsync();

            HandleIfAndError(message, responseJson);

            SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

            return spotifyResponse;
        }

        public async Task<SpotifyResponse> PostAsync(string path, Dictionary<string, string> data)
        {
            string url = string.Format("{0}{1}", _baseUrl, path);

            HttpClient client = new HttpClient();

            HttpContent httpContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage message = await client.PostAsync(url, httpContent);

            string responseJson = await message.Content.ReadAsStringAsync();

            HandleIfAndError(message, responseJson);

            SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

            return spotifyResponse;
        }

        public async Task<SpotifyResponse> PostAsync(string baseUrl, string path, Dictionary<string, string> data)
        {
            string url = string.Format("{0}{1}", baseUrl, path);

            HttpClient client = new HttpClient();

            HttpContent httpContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage message = await client.PostAsync(url, httpContent);

            string responseJson = await message.Content.ReadAsStringAsync();

            HandleIfAndError(message, responseJson);

            SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

            return spotifyResponse;
        }


        public async Task<SpotifyResponse> PostAsync(string path, string data, string accessToken, bool bearer)
        {
            string url = string.Format("{0}{1}", _baseUrl, path);

            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpResponseMessage message = await client.PostAsync(url, new StringContent(data));

            string responseJson = await message.Content.ReadAsStringAsync();

            HandleIfAndError(message, responseJson);

            SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

            return spotifyResponse;
        }

        public async Task<SpotifyResponse> PostAsync(string path, Dictionary<string, string> data, string accessToken, bool bearer)
        {
            string url = string.Format("{0}{1}", _baseUrl, path);

            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage message = await client.PostAsync(url, formUrlEncodedContent);

            string responseJson = await message.Content.ReadAsStringAsync();

            HandleIfAndError(message, responseJson);

            SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

            return spotifyResponse;
        }

        public async Task<SpotifyResponse> PutAsync(string path, Dictionary<string, string> data)
        {
            string url = string.Format("{0}{1}", _baseUrl, path);

            HttpClient client = new HttpClient();

            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage message = await client.PutAsync(url, formUrlEncodedContent);

            string responseJson = await message.Content.ReadAsStringAsync();

            HandleIfAndError(message, responseJson);

            SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

            return spotifyResponse;
        }

        public async Task<SpotifyResponse> PutAsync(string path, Dictionary<string, string> data, string accessToken, bool bearer)
        {
            string url = string.Format("{0}{1}", _baseUrl, path);

            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage message = await client.PutAsync(url, formUrlEncodedContent);

            string responseJson = await message.Content.ReadAsStringAsync();

            HandleIfAndError(message, responseJson);

            SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

            return spotifyResponse;
        }

        public async Task<SpotifyResponse> DeleteAsync(string path, string accessToken, bool bearer)
        {
            string url = string.Format("{0}{1}", _baseUrl, path);

            HttpClient client = new HttpClient();

            HttpResponseMessage message = await client.DeleteAsync(url);

            string responseJson = await message.Content.ReadAsStringAsync();

            HandleIfAndError(message, responseJson);

            SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

            return spotifyResponse;
        }

        public async Task<SpotifyResponse> DeleteAsync(string path, string data, string accessToken, bool bearer)
        {
            string url = string.Format("{0}{1}", _baseUrl, path);

            HttpClient client = new HttpClient();
            
            SetAccessToken(client, accessToken, bearer);

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, url) { Content = new StringContent(data) };

            HttpResponseMessage message = await client.SendAsync(requestMessage);

            string responseJson = await message.Content.ReadAsStringAsync();

            HandleIfAndError(message, responseJson);

            SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

            return spotifyResponse;
        }

        private void HandleIfAndError(HttpResponseMessage message, string responseJson)
        {
            if (message.StatusCode < HttpStatusCode.OK || message.StatusCode >= HttpStatusCode.BadRequest)
            {
                throw new SpotifyException(message.StatusCode, responseJson);
            }
        }

        private AuthenticationHeaderValue GetAuthenticationHeaderValue(string accessToken, bool bearer)
        {
            return bearer ? new AuthenticationHeaderValue("Bearer", accessToken) : new AuthenticationHeaderValue(accessToken);
        }
        private void SetAccessToken(HttpClient client, string accessToken, bool bearer)
        {
            client.DefaultRequestHeaders.Authorization = GetAuthenticationHeaderValue(accessToken, bearer);
        }
    }
}