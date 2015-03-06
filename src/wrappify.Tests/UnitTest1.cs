using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace wrappify.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private IRequestConfiguration Configuration;
        private WebApiRequest Request;
        private Spotify Spotify;
        [TestInitialize]
        public void Init()
        {
            Configuration = new RequestConfiguration()
                            {
                                Host = "api.spotify.com",
                                Port = "443",
                                Scheme = "https"
                            };

            Request = new WebApiRequest(Configuration);
            Spotify = new Spotify(Request);
        }

        [TestMethod]
        public async void GetMeTest()
        {
            var user = await Spotify.GetMe("v1/me");
        }


        [TestMethod]
        public async void SearchTest()
        {
             Spotify.Search("v1/search");
        }
    }


}
