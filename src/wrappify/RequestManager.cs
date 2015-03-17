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
        private readonly IRequestConfiguration _requestConfiguration;
        public string Url { get; set; }
        public RequestManager(IRequestConfiguration requestConfiguration)
        {
            _requestConfiguration = requestConfiguration;
            
        }

        public async Task<string> Get(string path)
        {
            Url = string.Format("{0}://{1}:{2}/{3}", _requestConfiguration.Scheme, _requestConfiguration.Host, _requestConfiguration.Port);

            HttpClient client = new HttpClient();

            HttpResponseMessage httpResponseMessage = await client.GetAsync(Url);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Get(string path, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpResponseMessage httpResponseMessage = await client.GetAsync(Url);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Post(string path, Dictionary<string, string> data)
        {
            HttpClient client = new HttpClient();

            HttpContent httpContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage httpResponseMessage = await client.PostAsync(Url, httpContent);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Post(string path, string data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpResponseMessage httpResponseMessage = await client.PostAsync(Url, new StringContent(data));

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Post(string path, Dictionary<string, string> data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage httpResponseMessage = await client.PostAsync(Url, formUrlEncodedContent);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Put(string path, Dictionary<string, string> data)
        {
            HttpClient client = new HttpClient();

            HttpContent httpContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage httpResponseMessage = await client.PutAsync(Url, httpContent);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Put(string path, Dictionary<string, string> data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(data.ToArray());

            HttpResponseMessage httpResponseMessage = await client.PutAsync(Url, formUrlEncodedContent);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Delete(string path, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpResponseMessage httpResponseMessage = await client.DeleteAsync(Url);
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        public async Task<string> Delete(string path, string data, string accessToken, bool bearer)
        {
            HttpClient client = new HttpClient();

            SetAccessToken(client, accessToken, bearer);

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, Url) { Content = new StringContent(data) };

            HttpResponseMessage httpResponseMessage = await client.SendAsync(message);

            return await httpResponseMessage.Content.ReadAsStringAsync();
        }
        private AuthenticationHeaderValue GetAuthenticationHeaderValue(string path, string accessToken, bool bearer)
        {
            return bearer ? new AuthenticationHeaderValue("Bearer", accessToken) : new AuthenticationHeaderValue(accessToken);
        }
        private void SetAccessToken(string path, HttpClient client, string accessToken, bool bearer)
        {
            client.DefaultRequestHeaders.Authorization = GetAuthenticationHeaderValue(accessToken, bearer);
        }
    }
}