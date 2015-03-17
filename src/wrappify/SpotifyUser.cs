using System.Threading.Tasks;
using Newtonsoft.Json;
using wrappify.Interfaces;
using wrappify.Models;

namespace wrappify
{
    public partial class Spotify
    {
        public async Task<User> GetUser(string userId)
        {
            const string path = "v1/users";

            SetPath(path);

            IRequestManager requestManager = new RequestManager(_url);

            string responseJson = await requestManager.Get(AccessToken, false);

            User user = JsonConvert.DeserializeObject<User>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return user;
        }
        public async Task<User> GetCurrentUserProfileTest()
        {
            const string path = "v1/me";

            SetPath(path);

            IRequestManager requestManager = new RequestManager(_url);

            string responseJson = await requestManager.Get(AccessToken, true);

            User user = JsonConvert.DeserializeObject<User>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return user;
        }
    }
}