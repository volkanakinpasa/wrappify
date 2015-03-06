using System.Threading.Tasks;
using Newtonsoft.Json;

namespace wrappify
{
    public class Spotify : ISpotify
    {
        private readonly WebApiRequest _request;

        public Spotify(WebApiRequest request)
        {
            _request = request;
        }

        public User GetUser(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetMe(string v1Me)
        {
            WebApiManager manager = new WebApiManager();
            string responseJson = await manager.PerformRequest(_request);
            User user = JsonConvert.DeserializeObject<User>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });
            return user;
        }

        public void Search(string v1Search)
        {
            throw new System.NotImplementedException();
        }
    }
}