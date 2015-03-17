using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using wrappify.Interfaces;
using wrappify.Models;
namespace wrappify
{
    public class SpotifyClient : ISpotifyClient
    {
        private readonly IRequestConfiguration _requestConfiguration;
        private readonly IRequestOperation _requestManager;
        private const string _host = "api.spotify.com";
        private const string _port = "443";
        private const string _scheme = "https";
        private string AccessToken { get; set; }

        public SpotifyClient(IRequestOperation requestManager, IRequestConfiguration requestConfiguration)
        {
            _requestManager = requestManager;
            _requestConfiguration = requestConfiguration;
        }

        public SpotifyClient(IRequestOperation requestManager)
            : this(requestManager, new RequestConfiguration(_host, _port, _scheme))
        {
            _requestManager = requestManager;
        }

        public async Task<Album> GetAnAlbum(string id)
        {
            string path = string.Format("v1/albums/{0}", id);

            string url = Helper.BuildUrl(_requestConfiguration, path);

            string responseJson = await _requestManager.Get(url);

            Album result = JsonConvert.DeserializeObject<Album>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return result;
        }
        public async Task<AlbumModel> GetSeveralAlbums(string ids)
        {
            string path = string.Format("v1/albums?ids={0}", ids);

            string url = Helper.BuildUrl(_requestConfiguration, path);

            string responseJson = await _requestManager.Get(url);

            AlbumModel result = JsonConvert.DeserializeObject<AlbumModel>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return result;
        }
        public async Task<Paging<TrackSimplified>> GetAnAlbumsTracks(string id, int limit = 20, int offset = 0)
        {
            string path = string.Format("v1/albums/{0}/tracks?limit={1}&offset={2}", id, limit == 0 ? 20 : 0, offset);

            string url = Helper.BuildUrl(_requestConfiguration, path);

            string responseJson = await _requestManager.Get(url);

            Paging<TrackSimplified> result = JsonConvert.DeserializeObject<Paging<TrackSimplified>>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return result;
        }
        public async Task<Paging<Playlist>> GetAUsersPlaylists(string userId, int limit = 0, int offset = 0)
        {
            string path = string.Format("v1/users/{0}/playlists?limit={1}&offset={2}", userId, limit, offset);

            string url = Helper.BuildUrl(_requestConfiguration, path);

            string responseJson = await _requestManager.Get(url, AccessToken, true);

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

            string url = Helper.BuildUrl(_requestConfiguration, path);

            string responseJson = await _requestManager.Get(url, AccessToken, true);

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

            string url = Helper.BuildUrl(_requestConfiguration, path);

            string responseJson = await _requestManager.Get(url, AccessToken, true);

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

            string url = Helper.BuildUrl(_requestConfiguration, path);

            Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "name", name }, { "public", isPublic ? "true" : "false" } };

            string serializedata = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

            string responseJson = await _requestManager.Post(url, serializedata, AccessToken, true);

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

            string url = Helper.BuildUrl(_requestConfiguration, path);

            Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>() { { "uris", trackUris } };

            string serializedata = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

            string responseJson = await _requestManager.Post(url, serializedata, AccessToken, true);

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

            string url = Helper.BuildUrl(_requestConfiguration, path);

            Dictionary<string, Dictionary<string, string>[]> arr = new Dictionary<string, Dictionary<string, string>[]>();

            Dictionary<string, string>[] array = trackUris.Select(s => new Dictionary<string, string> { { "uri", s } }).ToArray();

            arr.Add("tracks", array);

            string serializedata = JsonConvert.SerializeObject(arr, Formatting.Indented);

            string responseJson = await _requestManager.Delete(url, serializedata, AccessToken, true);

            PostResult result = JsonConvert.DeserializeObject<PostResult>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return result;
        }
        public async Task<Track> GetATrack(string trackId)
        {
            string path = string.Format("v1/tracks/{0}", trackId);

            string url = Helper.BuildUrl(_requestConfiguration, path);

            string responseJson = await _requestManager.Get(url);

            Track track = JsonConvert.DeserializeObject<Track>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return track;
        }
        public async Task<TrackResult> GetSeveralTracks(string trackIds)
        {
            string path = string.Format("v1/tracks?ids={0}", trackIds);

            string url = Helper.BuildUrl(_requestConfiguration, path);

            string responseJson = await _requestManager.Get(url);

            TrackResult trackResult = JsonConvert.DeserializeObject<TrackResult>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return trackResult;
        }
        public async Task<User> GetUser(string userId)
        {
            const string path = "v1/users";

            string url = Helper.BuildUrl(_requestConfiguration, path);

            string responseJson = await _requestManager.Get(url, AccessToken, false);

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

            string url = Helper.BuildUrl(_requestConfiguration, path);

            string responseJson = await _requestManager.Get(url, AccessToken, true);

            User user = JsonConvert.DeserializeObject<User>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return user;
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

            string json = await _requestManager.Post(url, postData);

            var obj = JsonConvert.DeserializeObject<AccessTokenModel>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return obj;
        }
        public void SetAccessToken(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}