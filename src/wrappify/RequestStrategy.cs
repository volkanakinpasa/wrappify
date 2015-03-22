using System.Threading.Tasks;
using wrappify.Responses;

namespace wrappify
{
    public interface RequestStrategy
    {
        Task<SpotifyResponse> GetAsync(string path);
    }
}
