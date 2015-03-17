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
        public async Task<Album> GetAnAlbum(string id)
        {
            string path = string.Format("v1/albums/{0}", id);

            string responseJson = await _requestManager.Get(path);

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

            string responseJson = await _requestManager.Get(path);

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

            string responseJson = await _requestManager.Get(path);

            Paging<TrackSimplified> result = JsonConvert.DeserializeObject<Paging<TrackSimplified>>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return result;
        }
    }
}