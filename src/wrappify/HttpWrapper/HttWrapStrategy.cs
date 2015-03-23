﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Httwrap;
using Httwrap.Auth;
using wrappify.Responses;

namespace wrappify.HttpWrapper
{
    public class HttWrapStrategy : IHttpWrapperStrategy
    {
        private readonly string _baseUrl;

        public HttWrapStrategy(RequestConfiguration requestConfiguration)
        {
            _baseUrl = Helper.BuildUrl(requestConfiguration);
        }

        public async Task<SpotifyResponse> GetAsync(string path)
        {

            throw new System.NotImplementedException();
        }

        public async Task<SpotifyResponse> GetAsync(string path, string accessToken, bool bearer)
        {
            throw new System.NotImplementedException();
             
        }

        public async Task<SpotifyResponse> PostAsync(string path, Dictionary<string, string> data)
        {
            throw new System.NotImplementedException();
        }

        public Task<SpotifyResponse> PostAsync(string url, string path, Dictionary<string, string> data)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SpotifyResponse> PostAsync(string path, string data, string accessToken, bool bearer)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SpotifyResponse> PostAsync(string path, Dictionary<string, string> data, string accessToken, bool bearer)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SpotifyResponse> PutAsync(string path, Dictionary<string, string> data)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SpotifyResponse> PutAsync(string path, Dictionary<string, string> data, string accessToken, bool bearer)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SpotifyResponse> DeleteAsync(string path, string accessToken, bool bearer)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SpotifyResponse> DeleteAsync(string path, string data, string accessToken, bool bearer)
        {
            throw new System.NotImplementedException();
        }
        private void SetAccessToken(HttpClient client, string accessToken, bool bearer)
        {
           
        }
    }
}