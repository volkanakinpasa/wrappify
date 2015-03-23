using System.Collections.Generic;
using System.Threading.Tasks;
using wrappify.Responses;

namespace wrappify.HttpWrapper
{
    public interface IHttpWrapperStrategy
    {
        Task<SpotifyResponse> GetAsync(string path);
        Task<SpotifyResponse> GetAsync(string path, string accessToken, bool bearer);
        Task<SpotifyResponse> PostAsync(string path, Dictionary<string, string> data);
        Task<SpotifyResponse> PostAsync(string url, string path, Dictionary<string, string> data);
        Task<SpotifyResponse> PostAsync(string path, string data, string accessToken, bool bearer);
        Task<SpotifyResponse> PostAsync(string path, Dictionary<string, string> data, string accessToken, bool bearer);
        Task<SpotifyResponse> PutAsync(string path, Dictionary<string, string> data);
        Task<SpotifyResponse> PutAsync(string path, Dictionary<string, string> data, string accessToken, bool bearer);
        Task<SpotifyResponse> DeleteAsync(string path, string accessToken, bool bearer);
        Task<SpotifyResponse> DeleteAsync(string path, string data, string accessToken, bool bearer);
    }
}
