using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using wrappify.Interfaces;
using wrappify.Models;
using wrappify.Responses;

namespace wrappify
{
    public class SpotifyClient : ISpotifyClient
    {
        private readonly IRequestConfiguration _requestConfiguration;
        private readonly IRequestManager _requestOperation;
        private const string _host = "api.spotify.com";
        private const string _port = "443";
        private const string _scheme = "https";
        private string AccessToken { get; set; }

        public SpotifyClient()
            : this(new RequestConfiguration(_host, _port, _scheme))
        {

        }

        public SpotifyClient(IRequestConfiguration requestConfiguration)
            : this(requestConfiguration, new RequestManager())
        {
        }

        public SpotifyClient(IRequestConfiguration requestConfiguration, IRequestManager requestOperation)
        {
            _requestConfiguration = requestConfiguration;
            _requestOperation = requestOperation;
        }

        public async Task<Album> GetAnAlbum(string id)
        {
            try
            {
                string path = string.Format("v1/albums/{0}", id);

                string url = Helper.BuildUrl(_requestConfiguration, path);

                HttpResponseMessage message = await _requestOperation.GetAsync(url);

                string responseJson = await message.Content.ReadAsStringAsync();

                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<Album>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }
        }
        public async Task<AlbumModel> GetSeveralAlbums(string ids)
        {
            try
            {
                string path = string.Format("v1/albums?ids={0}", ids);

                string url = Helper.BuildUrl(_requestConfiguration, path);

                HttpResponseMessage message = await _requestOperation.GetAsync(url);

                string responseJson = await message.Content.ReadAsStringAsync();

                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<AlbumModel>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }
        }
        public async Task<Paging<TrackSimplified>> GetAnAlbumsTracks(string id, int limit = 20, int offset = 0)
        {
            try
            {
                string path = string.Format("v1/albums/{0}/tracks?limit={1}&offset={2}", id, limit == 0 ? 20 : 0, offset);

                string url = Helper.BuildUrl(_requestConfiguration, path);

                HttpResponseMessage message = await _requestOperation.GetAsync(url);

                string responseJson = await message.Content.ReadAsStringAsync();

                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<Paging<TrackSimplified>>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }

        }
        public async Task<Paging<Playlist>> GetAUsersPlaylists(string userId, int limit = 0, int offset = 0)
        {
            try
            {
                string path = string.Format("v1/users/{0}/playlists?limit={1}&offset={2}", userId, limit, offset);

                string url = Helper.BuildUrl(_requestConfiguration, path);

                HttpResponseMessage message = await _requestOperation.GetAsync(url, AccessToken, true);

                string responseJson = await message.Content.ReadAsStringAsync();

                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<Paging<Playlist>>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }

        }
        public async Task<Playlist> GetAPlaylist(string userId, string playlistId, string fields = null)
        {
            try
            {
                string path = string.Format("v1/users/{0}/playlists/{1}?fields={2}", userId, playlistId, fields ?? "");

                string url = Helper.BuildUrl(_requestConfiguration, path);

                HttpResponseMessage message = await _requestOperation.GetAsync(url, AccessToken, true);

                string responseJson = await message.Content.ReadAsStringAsync();

                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<Playlist>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }
        }
        public async Task<Paging<playlisttrack>> GetAPlaylistsTracks(string userId, string playlistId, string fields = null)
        {
            try
            {
                string path = string.Format("v1/users/{0}/playlists/{1}/tracks?fields={2}", userId, playlistId, fields ?? "");

                string url = Helper.BuildUrl(_requestConfiguration, path);

                HttpResponseMessage message = await _requestOperation.GetAsync(url, AccessToken, true);

                string responseJson = await message.Content.ReadAsStringAsync();

                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<Paging<playlisttrack>>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }
        }
        public async Task<Playlist> CreateAPlaylist(string userId, string name, bool isPublic = true)
        {
            try
            {
                string path = string.Format("v1/users/{0}/playlists", userId);

                string url = Helper.BuildUrl(_requestConfiguration, path);

                Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "name", name }, { "public", isPublic ? "true" : "false" } };

                string serializedata = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

                HttpResponseMessage message = await _requestOperation.PostAsync(url, serializedata, AccessToken, true);
                string responseJson = await message.Content.ReadAsStringAsync();

                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<Playlist>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }
        }
        public async Task<PostResult> AddTracksToAPlaylist(string userId, string playlistId, string[] trackUris)
        {
            try
            {
                string path = string.Format("v1/users/{0}/playlists/{1}/tracks", userId, playlistId);

                string url = Helper.BuildUrl(_requestConfiguration, path);

                Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>() { { "uris", trackUris } };

                string serializedata = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

                HttpResponseMessage message = await _requestOperation.PostAsync(url, serializedata, AccessToken, true);

                string responseJson = await message.Content.ReadAsStringAsync();

                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<PostResult>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }
        }
        public async Task<PostResult> RemoveATrack(string userId, string playlistId, List<string> trackUris)
        {
            try
            {
                string path = string.Format("v1/users/{0}/playlists/{1}/tracks", userId, playlistId);

                string url = Helper.BuildUrl(_requestConfiguration, path);

                Dictionary<string, Dictionary<string, string>[]> arr = new Dictionary<string, Dictionary<string, string>[]>();

                Dictionary<string, string>[] array = trackUris.Select(s => new Dictionary<string, string> { { "uri", s } }).ToArray();

                arr.Add("tracks", array);

                string serializedata = JsonConvert.SerializeObject(arr, Formatting.Indented);

                HttpResponseMessage message = await _requestOperation.DeleteAsync(url, serializedata, AccessToken, true);

                string responseJson = await message.Content.ReadAsStringAsync();

                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<PostResult>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }
        }
        public async Task<Track> GetATrack(string trackId)
        {
            try
            {
                string path = string.Format("v1/tracks/{0}", trackId);

                string url = Helper.BuildUrl(_requestConfiguration, path);

                HttpResponseMessage message = await _requestOperation.GetAsync(url);
                string responseJson = await message.Content.ReadAsStringAsync();
                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<Track>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }
        }
        public async Task<TrackResult> GetSeveralTracks(string trackIds)
        {
            try
            {
                string path = string.Format("v1/tracks?ids={0}", trackIds);

                string url = Helper.BuildUrl(_requestConfiguration, path);

                HttpResponseMessage message = await _requestOperation.GetAsync(url);

                string responseJson = await message.Content.ReadAsStringAsync();

                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<TrackResult>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }
        }
        public async Task<User> GetUser(string userId)
        {
            try
            {
                const string path = "v1/users";

                string url = Helper.BuildUrl(_requestConfiguration, path);

                HttpResponseMessage message = await _requestOperation.GetAsync(url, AccessToken, false);

                string responseJson = await message.Content.ReadAsStringAsync();

                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<User>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }
        }
        public async Task<User> GetCurrentUserProfileTest()
        {
            try
            {
                const string path = "v1/me";

                string url = Helper.BuildUrl(_requestConfiguration, path);

                HttpResponseMessage message = await _requestOperation.GetAsync(url, AccessToken, true);
                string responseJson = await message.Content.ReadAsStringAsync();
                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<User>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }
        }
        public async Task<AccessTokenModel> GetAccessToken(string code, string redirectUri, string clientSecret, string clientId)
        {
            try
            {
                Dictionary<string, string> postData = new Dictionary<string, string>
                                                  {
                                                      {"code", code},
                                                      {"grant_type", "authorization_code"},
                                                      {"redirect_uri", redirectUri},
                                                      {"client_secret", clientSecret},
                                                      {"client_id", clientId}
                                                  };

                string url = string.Format("https://accounts.spotify.com/api/token");

                HttpResponseMessage message = await _requestOperation.PostAsync(url, postData);

                string responseJson = await message.Content.ReadAsStringAsync();

                HandleIfAndError(message, responseJson);

                SpotifyResponse spotifyResponse = new SpotifyResponse(responseJson, message.StatusCode);

                return spotifyResponse.Result<AccessTokenModel>();
            }
            catch (HttpRequestException ex)
            {
                throw new SpotifyException(ex);
            }

        }

        public string GetAuthorizeUrl(string clientId, string redirectUri, string scope)
        {
            return string.Format("https://accounts.spotify.com/authorize/?client_id={0}&response_type=code&redirect_uri={1}&scope={2}", clientId, redirectUri, scope);
        }
        public void SetAccessToken(string accessToken)
        {
            AccessToken = accessToken;
        }

        private void HandleIfAndError(HttpResponseMessage message, string responseJson)
        {
            if (message.StatusCode < HttpStatusCode.OK || message.StatusCode >= HttpStatusCode.BadRequest)
            {
                throw new SpotifyException(message.StatusCode, responseJson);
            }
        }
    }
}