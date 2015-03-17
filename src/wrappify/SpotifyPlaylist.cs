using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using wrappify.Interfaces;
using wrappify.Models;
namespace wrappify
{
    public partial class Spotify
    {
        public async Task<Paging<Playlist>> GetAUsersPlaylists(string userId, int limit = 0, int offset = 0)
        {
            string path = string.Format("v1/users/{0}/playlists?limit={1}&offset={2}", userId, limit, offset);

            string responseJson = await _requestManager.Get(path, AccessToken, true);

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



            string responseJson = await _requestManager.Get(path, AccessToken, true);

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



            string responseJson = await _requestManager.Get(path, AccessToken, true);

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


            Dictionary<string, string> dictionary = new Dictionary<string, string>() { { "name", name }, { "public", isPublic ? "true" : "false" } };

            string serializedata = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

            string responseJson = await _requestManager.Post(path, serializedata, AccessToken, true);

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



            Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>() { { "uris", trackUris } };

            string serializedata = JsonConvert.SerializeObject(dictionary, Formatting.Indented);

            string responseJson = await _requestManager.Post(path, serializedata, AccessToken, true);

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

            Dictionary<string, Dictionary<string, string>[]> arr = new Dictionary<string, Dictionary<string, string>[]>();

            Dictionary<string, string>[] array = trackUris.Select(s => new Dictionary<string, string> { { "uri", s } }).ToArray();

            arr.Add("tracks", array);

            string serializedata = JsonConvert.SerializeObject(arr, Formatting.Indented);

            string responseJson = await _requestManager.Delete(path, serializedata, AccessToken, true);

            PostResult result = JsonConvert.DeserializeObject<PostResult>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return result;
        }
    }
}