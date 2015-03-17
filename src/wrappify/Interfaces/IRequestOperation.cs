using System.Collections.Generic;
using System.Threading.Tasks;

namespace wrappify.Interfaces
{
    public interface IRequestOperation
    {
        Task<string> Get(string url);
        Task<string> Get(string url, string accessToken, bool bearer);
        Task<string> Post(string url, Dictionary<string, string> data);
        Task<string> Post(string url, string data, string accessToken, bool bearer);
        Task<string> Post(string url, Dictionary<string, string> data, string accessToken, bool bearer);
        Task<string> Put(string url, Dictionary<string, string> data);
        Task<string> Put(string url, Dictionary<string, string> data, string accessToken, bool bearer);
        Task<string> Delete(string url, string accessToken, bool bearer);
        Task<string> Delete(string url, string data, string accessToken, bool bearer);
    }
}