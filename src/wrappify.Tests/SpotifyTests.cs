using System.Collections.Generic;
using System.Linq;
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
        private Mock<IRequestManager> _requestManagerMock;
        private Mock<IRequestConfiguration> _requestConfigurationMock;



        [TestInitialize]
        public void Init()
        {
            _requestManagerMock = new Mock<IRequestManager>();
            _requestConfigurationMock = new Mock<IRequestConfiguration>();
            _spotifyClient = new SpotifyClient(_requestConfigurationMock.Object, _requestManagerMock.Object);
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task GetAnAlbumTest()
        {
            Album fakeModel = _fixture
                .Build<Album>()
                .With(album => album.Tracks, new Paging<Track>())
                .Create();

            string serializedAlbum = JsonConvert.SerializeObject(fakeModel);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(serializedAlbum) };

            _requestManagerMock.Setup(manager => manager.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(message)).Verifiable();

            Album result = await _spotifyClient.GetAnAlbum("07DseFAuj1KMp807W9XZVl");

            result.Id.Should().Be(fakeModel.Id);

            _requestManagerMock.VerifyAll();
        }

        [TestMethod]
        public async Task GetCurrentUserProfileTest()
        {
            User fakeModel = _fixture.Create<User>();

            string serializedAlbum = JsonConvert.SerializeObject(fakeModel);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(serializedAlbum) };

            _requestManagerMock.Setup(manager => manager.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(Task.FromResult(message)).Verifiable();

            _spotifyClient.SetAccessToken(_fixture.Create<string>("token"));

            User result = await _spotifyClient.GetCurrentUserProfileTest();

            result.Should().NotBeNull();

            result.Id.Should().Be(fakeModel.Id);

            _requestManagerMock.VerifyAll();
        }

        [TestMethod]
        public async Task GetAnAlbumsTracks()
        {
            Paging<TrackSimplified> fakeModel = _fixture
                .Build<Paging<TrackSimplified>>()
                .Create();
            string serialized = JsonConvert.SerializeObject(fakeModel);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(serialized) };

            _requestManagerMock.Setup(manager => manager.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(message)).Verifiable();

            Paging<TrackSimplified> result = await _spotifyClient.GetAnAlbumsTracks("07DseFAuj1KMp807W9XZVl");

            result.Items.Count.Should().BeGreaterThan(0);

            result.Total.Should().Be(fakeModel.Total);

            result.Items.FirstOrDefault().Id.Should().Be(fakeModel.Items.FirstOrDefault().Id);
        }

        [TestMethod]
        public async Task GetAccessToken()
        {
            const string client_id = "cliet_id";
            const string client_secret = "client_secet";
            const string redirect_uri = "http://localhost/callback";
            const string code = "";

            AccessTokenModel fakeModel = _fixture
               .Build<AccessTokenModel>()
               .Create();

            string serialized = JsonConvert.SerializeObject(fakeModel);

            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(serialized) };

            _requestManagerMock.Setup(manager => manager.PostAsync(It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult(message)).Verifiable();

            AccessTokenModel accesstoken = await _spotifyClient.GetAccessToken(code, redirect_uri, client_secret, client_id);

            accesstoken.AccessToken.Should().Be(fakeModel.AccessToken);
        }
    }
}
