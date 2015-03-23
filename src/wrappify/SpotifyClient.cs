using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using wrappify.HttpWrapper;
using wrappify.Interfaces;
using wrappify.Models;
using wrappify.Responses;

namespace wrappify
{
    public class SpotifyClient : ISpotifyClient
    {
        private readonly IHttpWrapperStrategy _httpWrapperStrategy;
        private string AccessToken { get; set; }

        public SpotifyClient(IHttpWrapperStrategy httpWrapperStrategy)
        {
            _httpWrapperStrategy = httpWrapperStrategy;
        }

        public async Task<Album> GetAnAlbum(string id)
        {
            try
            {
                string path = string.Format("v1/albums/{0}", id);

                SpotifyResponse response = await _httpWrapperStrategy.GetAsync(path);

                return response.Result<Album>();
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

                SpotifyResponse response = await _httpWrapperStrategy.GetAsync(path);

                return response.Result<AlbumModel>();
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

                SpotifyResponse response = await _httpWrapperStrategy.GetAsync(path);

                return response.Result<Paging<TrackSimplified>>();
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

                SpotifyResponse response = await _httpWrapperStrategy.GetAsync(path, AccessToken, true);

                return response.Result<Paging<Playlist>>();
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

                SpotifyResponse response = await _httpWrapperStrategy.GetAsync(path, AccessToken, true);

                return response.Result<Playlist>();
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

                SpotifyResponse response = await _httpWrapperStrategy.GetAsync(path, AccessToken, true);

                return response.Result<Paging<playlisttrack>>();
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

                Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "name", name }, { "public", isPublic ? "true" : "false" } };

                string serializedata = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

                SpotifyResponse response = await _httpWrapperStrategy.PostAsync(path, serializedata, AccessToken, true);

                return response.Result<Playlist>();
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

                Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>() { { "uris", trackUris } };

                string serializedata = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

                SpotifyResponse response = await _httpWrapperStrategy.PostAsync(path, serializedata, AccessToken, true);

                return response.Result<PostResult>();
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

                Dictionary<string, Dictionary<string, string>[]> arr = new Dictionary<string, Dictionary<string, string>[]>()
                {
                    { "tracks", trackUris.Select(s => new Dictionary<string, string> { { "uri", s } }).ToArray() }
                };

                string serializedata = JsonConvert.SerializeObject(arr, Formatting.Indented);

                SpotifyResponse response = await _httpWrapperStrategy.DeleteAsync(path, serializedata, AccessToken, true);

                return response.Result<PostResult>();
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

                SpotifyResponse response = await _httpWrapperStrategy.GetAsync(path);

                return response.Result<Track>();
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

                SpotifyResponse response = await _httpWrapperStrategy.GetAsync(path);

                return response.Result<TrackResult>();
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

                SpotifyResponse response = await _httpWrapperStrategy.GetAsync(path, AccessToken, false);

                return response.Result<User>();
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

                SpotifyResponse response = await _httpWrapperStrategy.GetAsync(path, AccessToken, true);

                return response.Result<User>();
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
                Dictionary<string, string> data = new Dictionary<string, string>
                                                  {
                                                      {"code", code},
                                                      {"grant_type", "authorization_code"},
                                                      {"redirect_uri", redirectUri},
                                                      {"client_secret", clientSecret},
                                                      {"client_id", clientId}
                                                  };

                string url = string.Format("https://accounts.spotify.com/");

                SpotifyResponse response = await _httpWrapperStrategy.PostAsync(url, "api/token", data);

                return response.Result<AccessTokenModel>();
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


    }
}