using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace wrappify.Interfaces
{
    public interface IRequestManager
    {
        Task<HttpResponseMessage> Get(string url);
        Task<HttpResponseMessage> Get(string url, string accessToken, bool bearer);
        Task<HttpResponseMessage> Post(string url, Dictionary<string, string> data);
        Task<HttpResponseMessage> Post(string url, string data, string accessToken, bool bearer);
        Task<HttpResponseMessage> Post(string url, Dictionary<string, string> data, string accessToken, bool bearer);
        Task<HttpResponseMessage> Put(string url, Dictionary<string, string> data);
        Task<HttpResponseMessage> Put(string url, Dictionary<string, string> data, string accessToken, bool bearer);
        Task<HttpResponseMessage> Delete(string url, string accessToken, bool bearer);
        Task<HttpResponseMessage> Delete(string url, string data, string accessToken, bool bearer);
    }
}