﻿using System.Threading.Tasks;
using Newtonsoft.Json;
using wrappify.Interfaces;
using wrappify.Models;

namespace wrappify
{
    public partial class Spotify
    {
        public async Task<Track> GetATrack(string trackId)
        {
            string path = string.Format("v1/tracks/{0}", trackId);

            SetPath(path);

            IRequestManager requestManager = new RequestManager(_url);

            string responseJson = await requestManager.Get();

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

            SetPath(path);

            IRequestManager requestManager = new RequestManager(_url);

            string responseJson = await requestManager.Get();

            TrackResult trackResult = JsonConvert.DeserializeObject<TrackResult>(responseJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });

            return trackResult;
        }
    }
}