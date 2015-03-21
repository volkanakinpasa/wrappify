using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace wrappify.Interfaces
{
    public interface IRequestManager
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> GetAsync(string url, string accessToken, bool bearer);
        Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> data);
        Task<HttpResponseMessage> PostAsync(string url, string data, string accessToken, bool bearer);
        Task<HttpResponseMessage> PostAsync(string url, Dictionary<string, string> data, string accessToken, bool bearer);
        Task<HttpResponseMessage> PutAsync(string url, Dictionary<string, string> data);
        Task<HttpResponseMessage> PutAsync(string url, Dictionary<string, string> data, string accessToken, bool bearer);
        Task<HttpResponseMessage> DeleteAsync(string url, string accessToken, bool bearer);
        Task<HttpResponseMessage> DeleteAsync(string url, string data, string accessToken, bool bearer);
    }
}