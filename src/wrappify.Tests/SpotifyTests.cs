using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
using wrappify.Interfaces;
using wrappify.Models;

namespace wrappify.Tests
{
    [TestClass]
    public class SpotifyTests
    {
        const string Host = "api.spotify.com";
        const string Port = "443";
        const string Scheme = "https";

        private ISpotifyClient _spotifyClient;
        private Fixture _fixture;
        private Mock<IRequestManager> _requestOperationMock;

        [TestInitialize]
        public void Init()
        {
            _requestOperationMock = new Mock<IRequestManager>();
            _spotifyClient = new SpotifyClient();
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task GetAnAlbumTest()
        {
            Album albumResult = _fixture
                .Build<Album>()
                .With(album => album.Tracks, new Paging<Track>())
                .Create();

            string serializedAlbum = JsonConvert.SerializeObject(albumResult);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(serializedAlbum) };

            _requestOperationMock.Setup(manager => manager.Get(It.IsAny<string>())).Returns(Task.FromResult(message)).Verifiable();

            Album result = await _spotifyClient.GetAnAlbum("07DseFAuj1KMp807W9XZVl");

            result.Id.Should().Be(albumResult.Id);

            _requestOperationMock.VerifyAll();
        }

        [TestMethod]
        public async Task GetCurrentUserProfileTest()
        {
            User user = _fixture.Create<User>();

            string serializedAlbum = JsonConvert.SerializeObject(user);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(serializedAlbum) };

            _requestOperationMock.Setup(manager => manager.Get(It.IsAny<string>())).Returns(Task.FromResult(message)).Verifiable();

            _spotifyClient.SetAccessToken(_fixture.Create<string>("token"));

            User result = await _spotifyClient.GetCurrentUserProfileTest();

            result.Should().NotBeNull();

            result.Id.Should().Be(user.Id);

            _requestOperationMock.VerifyAll();
        }


        [TestMethod]
        public async Task GetAnAlbumsTracks()
        {
            Paging<TrackSimplified> tracks = await _spotifyClient.GetAnAlbumsTracks("07DseFAuj1KMp807W9XZVl");
        }
        [TestMethod]
        public async Task GetaUserProfileTest()
        {
            _spotifyClient.SetAccessToken("");
            User user = await _spotifyClient.GetUser("volkanakinpasa");
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
            AccessTokenModel accesstoken = await _spotifyClient.GetAccessToken(code, redirect_uri, client_secret, client_id);
            Assert.IsNotNull(accesstoken);
            Assert.IsFalse(string.IsNullOrEmpty(accesstoken.AccessToken));
        }

        [TestMethod]
        public async Task GetaUsersPlaylists()
        {
            SetAccessToken();
            Paging<Playlist> playlistPaging = await _spotifyClient.GetAUsersPlaylists("volkanakinpasa");
        }

        [TestMethod]
        public async Task GetaPlaylists()
        {
            SetAccessToken();
            Playlist playlist = await _spotifyClient.GetAPlaylist("volkanakinpasa", "307NUmeWHrAxVMQ7X9Rxuu");



        }
        [TestMethod]
        public async Task GetaPlaylistsTracks()
        {
            SetAccessToken();
            Paging<playlisttrack> playlist = await _spotifyClient.GetAPlaylistsTracks("volkanakinpasa", "307NUmeWHrAxVMQ7X9Rxuu");
        }

        [TestMethod]
        public async Task CreateaPlaylist()
        {
            SetAccessToken();
            Playlist playlist = await _spotifyClient.CreateAPlaylist("volkanakinpasa", "Test Playlist Name", false);
        }

        [TestMethod]
        public async Task AddTracksToaPlaylist()
        {
            SetAccessToken();
            string[] trackuris = { "spotify:track:2CeFP3pggzhuzZB9rLMHYW", "spotify:track:3vlVbJmvSm3x5Hqmnzh8HI" };
            PostResult playlist = await _spotifyClient.AddTracksToAPlaylist("volkanakinpasa", "307NUmeWHrAxVMQ7X9Rxuu", trackuris);
        }

        [TestMethod]
        public async Task RemoveaTrack()
        {
            SetAccessToken();
            PostResult playlist = await _spotifyClient.RemoveATrack("volkanakinpasa", "0Z3z2n85fFi4qijYWVVUIQ",
                new List<string>() { "spotify:track:4V3kv5FPz8YE28SHyJjm6j" });

        }

        [TestMethod]
        public async Task GetaTrack()
        {
            await _spotifyClient.GetATrack("3g0XVm6ZTWHbtTTfKhmMo7");
        }

        [TestMethod]
        public async Task GetSeveralTracks()
        {
            await _spotifyClient.GetSeveralTracks("3g0XVm6ZTWHbtTTfKhmMo7,2eg2gvPXuwZ9FyrPaLgrXi");
        }

        private void SetAccessToken()
        {
            _spotifyClient.SetAccessToken("BQBvDTSjLKNmFTEYTFu5G0kdcVHhHgOeUhsjpXRUN_AzX_cf9pyXvS_NokT98ycBbiFesfEMgAwsR7gIXTZu11qvbwvOftzloSAsQkXjJ421Fkcrft8YvhoaxCkUUA7jmj8fI3Yqikt8zqyr5UMwbjzFU84unHiEZSov_IkalCCQ0g-x7ArwGiDqjdeD_DtmMWKOJLf5vJa_vs-CSoMj3WK1N258onyLc7jJLmxT1nnIX7u3cV2jj828fOftTCsAicMzV1pSeSYlmvKywmrjbRTKfWbteAkRRN47o9KE4GrPk_5B");
        }
    }
}
