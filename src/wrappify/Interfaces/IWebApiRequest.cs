using System.Collections.Generic;
using System.Threading.Tasks;

namespace wrappify.Interfaces
{
    public interface IWebApiRequest
    {
        Task<string> Get();
        Task<string> Get(string accessToken, bool bearer);
        Task<string> Post(Dictionary<string, string> data);
        Task<string> Post(string data, string accessToken, bool bearer);
        Task<string> Post(Dictionary<string, string> data, string accessToken, bool bearer);
        Task<string> Put(Dictionary<string, string> data);
        Task<string> Put(Dictionary<string, string> data, string accessToken, bool bearer);
        Task<string> Delete(string accessToken, bool bearer);
        Task<string> Delete(string data, string accessToken, bool bearer);
    }
}