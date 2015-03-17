using System.Collections.Generic;
using System.Threading.Tasks;

namespace wrappify.Interfaces
{
    public interface IRequestManager
    {
        Task<string> Get(string path);
        Task<string> Get(string path, string accessToken, bool bearer);
        Task<string> Post(string path, Dictionary<string, string> data);
        Task<string> Post(string path, string data, string accessToken, bool bearer);
        Task<string> Post(string path, Dictionary<string, string> data, string accessToken, bool bearer);
        Task<string> Put(string path, Dictionary<string, string> data);
        Task<string> Put(string path, Dictionary<string, string> data, string accessToken, bool bearer);
        Task<string> Delete(string path, string accessToken, bool bearer);
        Task<string> Delete(string path, string data, string accessToken, bool bearer);
    }
}