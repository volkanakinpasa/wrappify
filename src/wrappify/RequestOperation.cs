using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using wrappify.Interfaces;

namespace wrappify
{
    public class RequestOperation : IRequestOperation
    {
        public async Task<string> Get(string url)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Get(string url, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Post(string url, Dictionary<string, string> data)
        {
            HttpClient client = new HttpClient();

            HttpContent httpContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage httpResponseMessage = await client.PostAsync(url, httpContent);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Post(string url, string data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpResponseMessage httpResponseMessage = await client.PostAsync(url, new StringContent(data));

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Post(string url, Dictionary<string, string> data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage httpResponseMessage = await client.PostAsync(url, formUrlEncodedContent);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Put(string url, Dictionary<string, string> data)
        {
            HttpClient client = new HttpClient();

            HttpContent httpContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage httpResponseMessage = await client.PutAsync(url, httpContent);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Put(string url, Dictionary<string, string> data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(data.ToArray());


            HttpResponseMessage httpResponseMessage = await client.PutAsync(url, formUrlEncodedContent);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Delete(string url, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpResponseMessage httpResponseMessage = await client.DeleteAsync(url);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Delete(string url, string data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, url) { Content = new StringContent(data) };

            HttpResponseMessage httpResponseMessage = await client.SendAsync(message);

            return await httpResponseMessage.Content.ReadAsStringAsync();
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