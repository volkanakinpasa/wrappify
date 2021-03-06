﻿using System.Collections.Generic;
using System.Threading.Tasks;
using wrappify.Models;

namespace wrappify.Interfaces
{
    public interface ISpotifyClient
    {
        Task<Album> GetAnAlbum(string id);
        Task<AlbumModel> GetSeveralAlbums(string ids);
        Task<Paging<TrackSimplified>> GetAnAlbumsTracks(string id, int limit = 0, int offset = 0);
        Task<User> GetUser(string userId);
        Task<User> GetCurrentUserProfileTest();
        Task<Paging<Playlist>> GetAUsersPlaylists(string userId, int limit = 0, int offset = 0);
        Task<Playlist> GetAPlaylist(string userId, string playlistId, string fields = null);
        Task<Paging<playlisttrack>> GetAPlaylistsTracks(string userId, string playlistId, string fields = null);
        Task<Playlist> CreateAPlaylist(string userId, string name, bool isPublic = true);
        Task<PostResult> AddTracksToAPlaylist(string userId, string playlistId, string[] trackUris);
        Task<PostResult> RemoveATrack(string userId, string playlistId, List<string> trackUris);
        Task<Track> GetATrack(string trackId);
        Task<TrackResult> GetSeveralTracks(string trackIds);
        string GetAuthorizeUrl(string clientId, string redirectUri, string scope);
        void SetAccessToken(string accessToken);
        Task<AccessTokenModel> GetAccessToken(string code, string redirectUri, string clientSecret, string clientId);
    }
}