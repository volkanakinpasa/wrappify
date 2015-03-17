using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using wrappify.Interfaces;
using wrappify.Models;

namespace wrappify
{
    public partial class Spotify
    {
        public void SetAccessToken(string accessToken)
        {
            this.AccessToken = accessToken;
        }
        public async Task<AccessTokenModel> GetAccessToken(string code, string _redirectUri, string _clientSecret, string _clientId)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>
                                                  {
                                                      {"code", code},
                                                      {"grant_type", "authorization_code"},
                                                      {"redirect_uri", _redirectUri},
                                                      {"client_secret", _clientSecret},
                                                      {"client_id", _clientId}
                                                  };

            string url = string.Format("https://accounts.spotify.com/api/token");

            IRequestManager requestManager = new RequestManager(_scheme, _host, _port);

            string json = await requestManager.Post("", postData);

            var obj = JsonConvert.DeserializeObject<AccessTokenModel>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return obj;
        }
    }
}