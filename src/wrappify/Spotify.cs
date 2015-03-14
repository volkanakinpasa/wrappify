using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using wrappify.Interfaces;
using wrappify.Models;

namespace wrappify
{
    public class Spotify : ISpotify
    {
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        const string _host = "api.spotify.com";
        const string _port = "443";
        const string _scheme = "https";
        private string AccessToken { get; set; }
        public Spotify()
        {

        }
        public Spotify(string scheme, string host, string port)
        {
            Scheme = scheme;
            Host = host;
            Port = port;
        }
        #region User
        public async Task<User> GetUser(string userId)
        {
            const string path = "v1/users";

            string url = string.Format("{0}://{1}:{2}/{3}", _scheme, _host, _port, path);

            IWebApiRequest request = new WebApiRequest(url);

            string responseJson = await request.Get(AccessToken, false);

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

            string url = string.Format("{0}://{1}:{2}/{3}", _scheme, _host, _port, path);

            IWebApiRequest request = new WebApiRequest(url);

            string responseJson = await request.Get(AccessToken, true);

            User user = JsonConvert.DeserializeObject<User>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return user;
        }
        #endregion
        #region Playlist
        public async Task<Paging<Playlist>> GetAUsersPlaylists(string userId, int limit = 0, int offset = 0)
        {
            string path = string.Format("v1/users/{0}/playlists?limit={1}&limit={2}", userId, limit, offset);

            string url = string.Format("{0}://{1}:{2}/{3}", _scheme, _host, _port, path);

            IWebApiRequest request = new WebApiRequest(url);

            string responseJson = await request.Get(AccessToken, true);

            Paging<Playlist> paging = JsonConvert.DeserializeObject<Paging<Playlist>>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return paging;
        }
        public async Task<Playlist> GetAPlaylist(string userId, string playlistId, string fields = null)
        {
            string path = string.Format("v1/users/{0}/playlists/{1}?fields={2}", userId, playlistId, fields ?? "");

            string url = string.Format("{0}://{1}:{2}/{3}", _scheme, _host, _port, path);

            IWebApiRequest request = new WebApiRequest(url);

            string responseJson = await request.Get(AccessToken, true);

            Playlist result = JsonConvert.DeserializeObject<Playlist>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return result;
        }
        public async Task<Paging<playlisttrack>> GetAPlaylistsTracks(string userId, string playlistId, string fields = null)
        {
            string path = string.Format("v1/users/{0}/playlists/{1}/tracks?fields={2}", userId, playlistId, fields ?? "");

            string url = string.Format("{0}://{1}:{2}/{3}", _scheme, _host, _port, path);

            IWebApiRequest request = new WebApiRequest(url);

            string responseJson = await request.Get(AccessToken, true);

            Paging<playlisttrack> paging = JsonConvert.DeserializeObject<Paging<playlisttrack>>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return paging;
        }
        public async Task<Playlist> CreateAPlaylist(string userId, string name, bool isPublic = true)
        {
            string path = string.Format("v1/users/{0}/playlists", userId);

            string url = string.Format("{0}://{1}:{2}/{3}", _scheme, _host, _port, path);

            IWebApiRequest request = new WebApiRequest(url);
            Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "name", name }, { "public", isPublic ? "true" : "false" } };

            string serializedata = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

            string responseJson = await request.Post(serializedata, AccessToken, true);

            Playlist result = JsonConvert.DeserializeObject<Playlist>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return result;
        }
        public async Task<PostResult> AddTracksToAPlaylist(string userId, string playlistId, string[] trackUris)
        {
            string path = string.Format("v1/users/{0}/playlists/{1}/tracks", userId, playlistId);

            string url = string.Format("{0}://{1}:{2}/{3}", _scheme, _host, _port, path);

            IWebApiRequest request = new WebApiRequest(url);

            Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>() { { "uris", trackUris } };

            string serializedata = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

            string responseJson = await request.Post(serializedata, AccessToken, true);

            PostResult result = JsonConvert.DeserializeObject<PostResult>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return result;
        }
        public async Task<PostResult> RemoveATrack(string userId, string playlistId, List<string> trackUris)
        {
            string path = string.Format("v1/users/{0}/playlists/{1}/tracks", userId, playlistId);

            string url = string.Format("{0}://{1}:{2}/{3}", _scheme, _host, _port, path);

            IWebApiRequest request = new WebApiRequest(url);

            Dictionary<string, Dictionary<string, string>[]> arr = new Dictionary<string, Dictionary<string, string>[]>();

            Dictionary<string, string>[] array = trackUris.Select(s => new Dictionary<string, string> { { "uri", s } }).ToArray();

            arr.Add("tracks", array);

            string serializedata = JsonConvert.SerializeObject(arr, Formatting.Indented);

            string responseJson = await request.Delete(serializedata, AccessToken, true);

            PostResult result = JsonConvert.DeserializeObject<PostResult>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return result;
        }

        #endregion
        #region Track
        public async Task<Track> GetATrack(string trackId)
        {
            const string path = "v1/tracks";

            string url = string.Format("{0}://{1}:{2}/{3}/{4}", _scheme, _host, _port, path, trackId);

            IWebApiRequest request = new WebApiRequest(url);

            string responseJson = await request.Get();

            Track track = JsonConvert.DeserializeObject<Track>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return track;
        }

        public async Task<TrackResult> GetSeveralTracks(string trackIds)
        {
            const string path = "v1/tracks";

            string url = string.Format("{0}://{1}:{2}/{3}?ids={4}", _scheme, _host, _port, path, trackIds);

            IWebApiRequest request = new WebApiRequest(url);

            string responseJson = await request.Get();

            TrackResult trackResult = JsonConvert.DeserializeObject<TrackResult>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return trackResult;
        } 
        #endregion
        #region Token
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

            IWebApiRequest request = new WebApiRequest(url);

            string json = await request.Post(postData);

            var obj = JsonConvert.DeserializeObject<AccessTokenModel>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return obj;
        } 
        #endregion
    }
}