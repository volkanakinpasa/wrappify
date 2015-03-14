using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using wrappify.Interfaces;

namespace wrappify
{
    public class WebApiRequest : IWebApiRequest
    {
        public string Url { get; set; }
        public WebApiRequest(string url)
        {
            Url = url;
        }
        public async Task<string> Get()
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage httpResponseMessage = await client.GetAsync(Url);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Get(string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpResponseMessage httpResponseMessage = await client.GetAsync(Url);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Post(Dictionary<string, string> data)
        {
            HttpClient client = new HttpClient();

            HttpContent httpContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage httpResponseMessage = await client.PostAsync(Url, httpContent);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Post(string data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpResponseMessage httpResponseMessage = await client.PostAsync(Url, new StringContent(data));

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Post(Dictionary<string, string> data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage httpResponseMessage = await client.PostAsync(Url, formUrlEncodedContent);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Put(Dictionary<string, string> data)
        {
            HttpClient client = new HttpClient();

            HttpContent httpContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage httpResponseMessage = await client.PutAsync(Url, httpContent);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Put(Dictionary<string, string> data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage httpResponseMessage = await client.PutAsync(Url, formUrlEncodedContent);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Delete(string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpResponseMessage httpResponseMessage = await client.DeleteAsync(Url);
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Delete(string data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, Url) { Content = new StringContent(data) };

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