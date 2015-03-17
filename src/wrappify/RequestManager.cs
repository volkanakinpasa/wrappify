using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using wrappify.Interfaces;

namespace wrappify
{
    public class RequestManager : IRequestManager
    {
        public async Task<HttpResponseMessage> Get(string url)
        {
            return await new HttpClient().GetAsync(url);
        }
        public async Task<HttpResponseMessage> Get(string url, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            return await client.GetAsync(url);
        }
        public async Task<HttpResponseMessage> Post(string url, Dictionary<string, string> data)
        {
            HttpClient client = new HttpClient();

            HttpContent httpContent = new FormUrlEncodedContent(data.ToArray());

            return await client.PostAsync(url, httpContent);
        }
        public async Task<HttpResponseMessage> Post(string url, string data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            return await client.PostAsync(url, new StringContent(data));
        }
        public async Task<HttpResponseMessage> Post(string url, Dictionary<string, string> data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(data.ToArray());

            return await client.PostAsync(url, formUrlEncodedContent);
        }
        public async Task<HttpResponseMessage> Put(string url, Dictionary<string, string> data)
        {
            HttpClient client = new HttpClient();

            HttpContent httpContent = new FormUrlEncodedContent(data.ToArray());

            return await client.PutAsync(url, httpContent);
        }
        public async Task<HttpResponseMessage> Put(string url, Dictionary<string, string> data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(data.ToArray());

            return await client.PutAsync(url, formUrlEncodedContent);
        }
        public async Task<HttpResponseMessage> Delete(string url, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            return await client.DeleteAsync(url);
        }
        public async Task<HttpResponseMessage> Delete(string url, string data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, url) { Content = new StringContent(data) };

            return await client.SendAsync(message);
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