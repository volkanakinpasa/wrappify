using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wrappify.Interfaces;
using wrappify.Models;

namespace wrappify.Tests
{
    [TestClass]
    //[Ignore]
    public class SpotifyTests
    {
        const string Host = "api.spotify.com";
        const string Port = "443";
        const string Scheme = "https";

        private ISpotify Spotify;

        [TestInitialize]
        public void Init()
        {
            Spotify = new Spotify(new RequestConfiguration(Scheme, Host, Port));
        }

        [TestMethod]
        public async Task GetAnAlbumTest()
        {
            Album album = await Spotify.GetAnAlbum("07DseFAuj1KMp807W9XZVl");
        }

        [TestMethod]
        public async Task GetSeveralAlbumsTest()
        {
            AlbumModel albumModel = await Spotify.GetSeveralAlbums("07DseFAuj1KMp807W9XZVl");
        }

        [TestMethod]
        public async Task GetAnAlbumsTracks()
        {
            Paging<TrackSimplified> tracks = await Spotify.GetAnAlbumsTracks("07DseFAuj1KMp807W9XZVl");
        }

        [TestMethod]
        public async Task GetCurrentUserProfileTest()
        {
            SetAccessToken();
            User user = await Spotify.GetCurrentUserProfileTest();
            Assert.IsNotNull(user);
            Assert.IsFalse(string.IsNullOrEmpty(user.Id));
        }

        [TestMethod]
        public async Task GetaUserProfileTest()
        {
            Spotify.SetAccessToken("");
            User user = await Spotify.GetUser("volkanakinpasa");
            Assert.IsNotNull(user);
            Assert.IsFalse(string.IsNullOrEmpty(user.Id));

            Assert.IsNotNull(user);
            Assert.IsFalse(string.IsNullOrEmpty(user.Id));

        }

        [TestMethod]
        public async Task GetAccessToken()
        {
            const string client_id = "aba4782305d0480f9dbe2b63a7a77b42";
            const string client_secret = "4f2585881e99474f92b0e67ea69b22d0";
            const string redirect_uri = "http://localhost/callback";
            const string code = "";
            AccessTokenModel accesstoken = await Spotify.GetAccessToken(code, redirect_uri, client_secret, client_id);
            Assert.IsNotNull(accesstoken);
            Assert.IsFalse(string.IsNullOrEmpty(accesstoken.AccessToken));
        }

        [TestMethod]
        public async Task GetaUsersPlaylists()
        {
            SetAccessToken();
            Paging<Playlist> playlistPaging = await Spotify.GetAUsersPlaylists("volkanakinpasa");
        }

        [TestMethod]
        public async Task GetaPlaylists()
        {
            SetAccessToken();
            Playlist playlist = await Spotify.GetAPlaylist("volkanakinpasa", "307NUmeWHrAxVMQ7X9Rxuu");



        }
        [TestMethod]
        public async Task GetaPlaylistsTracks()
        {
            SetAccessToken();
            Paging<playlisttrack> playlist = await Spotify.GetAPlaylistsTracks("volkanakinpasa", "307NUmeWHrAxVMQ7X9Rxuu");
        }

        [TestMethod]
        public async Task CreateaPlaylist()
        {
            SetAccessToken();
            Playlist playlist = await Spotify.CreateAPlaylist("volkanakinpasa", "Test Playlist Name", false);
        }

        [TestMethod]
        public async Task AddTracksToaPlaylist()
        {
            SetAccessToken();
            string[] trackuris = { "spotify:track:2CeFP3pggzhuzZB9rLMHYW", "spotify:track:3vlVbJmvSm3x5Hqmnzh8HI" };
            PostResult playlist = await Spotify.AddTracksToAPlaylist("volkanakinpasa", "307NUmeWHrAxVMQ7X9Rxuu", trackuris);
        }

        [TestMethod]
        public async Task RemoveaTrack()
        {
            SetAccessToken();
            PostResult playlist = await Spotify.RemoveATrack("volkanakinpasa", "0Z3z2n85fFi4qijYWVVUIQ",
                new List<string>() { "spotify:track:4V3kv5FPz8YE28SHyJjm6j" });

        }

        [TestMethod]
        public async Task GetaTrack()
        {
            await Spotify.GetATrack("3g0XVm6ZTWHbtTTfKhmMo7");
        }

        [TestMethod]
        public async Task GetSeveralTracks()
        {
            await Spotify.GetSeveralTracks("3g0XVm6ZTWHbtTTfKhmMo7,2eg2gvPXuwZ9FyrPaLgrXi");
        }

        private void SetAccessToken()
        {
            Spotify.SetAccessToken("BQBvDTSjLKNmFTEYTFu5G0kdcVHhHgOeUhsjpXRUN_AzX_cf9pyXvS_NokT98ycBbiFesfEMgAwsR7gIXTZu11qvbwvOftzloSAsQkXjJ421Fkcrft8YvhoaxCkUUA7jmj8fI3Yqikt8zqyr5UMwbjzFU84unHiEZSov_IkalCCQ0g-x7ArwGiDqjdeD_DtmMWKOJLf5vJa_vs-CSoMj3WK1N258onyLc7jJLmxT1nnIX7u3cV2jj828fOftTCsAicMzV1pSeSYlmvKywmrjbRTKfWbteAkRRN47o9KE4GrPk_5B");
        }
    }
}
