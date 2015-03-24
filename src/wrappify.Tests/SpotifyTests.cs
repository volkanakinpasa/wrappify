using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
using wrappify.Interfaces;
using wrappify.Models;
using wrappify.Responses;
using wrappify.Wrappers;

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
        private Mock<IHttpWrapper> _httpWrapMock;

        [TestInitialize]
        public void Init()
        {
            _httpWrapMock = new Mock<IHttpWrapper>();

            _spotifyClient = new SpotifyClient(_httpWrapMock.Object);

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

            SpotifyResponse response = new SpotifyResponse(serializedAlbum, HttpStatusCode.OK);

            _httpWrapMock.Setup(manager => manager.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response)).Verifiable();

            Album result = await _spotifyClient.GetAnAlbum("07DseFAuj1KMp807W9XZVl");

            result.Id.Should().Be(fakeModel.Id);

            _httpWrapMock.VerifyAll();
        }

        [TestMethod]
        public async Task GetCurrentUserProfileTest()
        {
            User fakeModel = _fixture.Create<User>();

            string serializedAlbum = JsonConvert.SerializeObject(fakeModel);

            SpotifyResponse response = new SpotifyResponse(serializedAlbum, HttpStatusCode.OK);

            _httpWrapMock.Setup(manager => manager.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(Task.FromResult(response)).Verifiable();

            _spotifyClient.SetAccessToken(_fixture.Create<string>("token"));

            User result = await _spotifyClient.GetCurrentUserProfileTest();

            result.Should().NotBeNull();

            result.Id.Should().Be(fakeModel.Id);

            _httpWrapMock.VerifyAll();
        }

        [TestMethod]
        public async Task GetAnAlbumsTracks()
        {
            Paging<TrackSimplified> fakeModel = _fixture
                .Build<Paging<TrackSimplified>>()
                .Create();
            string serialized = JsonConvert.SerializeObject(fakeModel);

            SpotifyResponse response = new SpotifyResponse(serialized, HttpStatusCode.OK);

            _httpWrapMock.Setup(manager => manager.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(response)).Verifiable();

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

            SpotifyResponse response = new SpotifyResponse(serialized, HttpStatusCode.OK);

            _httpWrapMock.Setup(manager => manager.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Dictionary<string, string>>())).Returns(Task.FromResult(response)).Verifiable();

            AccessTokenModel accesstoken = await _spotifyClient.GetAccessToken(code, redirect_uri, client_secret, client_id);

            accesstoken.AccessToken.Should().Be(fakeModel.AccessToken);
        }

        [TestMethod]
        //[Ignore]
        public async Task GetAnAlbumTest_Wit_wrapper_Strategy()
        {
            RequestConfiguration requestConfiguration = new RequestConfiguration(Host, Port, Scheme);
            IHttpWrapper httpWrapperStrategy = new HttpWrapper(requestConfiguration);
            ISpotifyClient client = new SpotifyClient(httpWrapperStrategy);
            Album album = await client.GetAnAlbum("07DseFAuj1KMp807W9XZVl");
        }
    }
}
