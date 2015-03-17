using wrappify.Interfaces;

namespace wrappify
{
    public partial class Spotify : ISpotify
    {
        private readonly IRequestManager _requestManager;

        ///// <summary>
        ///// optional
        ///// </summary>
        //public string Path { get; set; }
        private const string _host = "api.spotify.com";
        private const string _port = "443";
        private const string _scheme = "https";

        private string AccessToken { get; set; }

        public Spotify()
            : this(new RequestConfiguration(_host, _port, _scheme))
        {

        }
        public Spotify(IRequestConfiguration requestConfiguration)
            : this(new RequestManager(requestConfiguration))
        {


        }
        public Spotify(IRequestManager requestManager)
        {
            _requestManager = requestManager;
        }
    }
}