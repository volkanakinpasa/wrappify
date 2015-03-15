using wrappify.Interfaces;

namespace wrappify
{
    public partial class Spotify : ISpotify
    {
        /// <summary>
        /// optional
        /// </summary>
        public string Path { get; set; }
        private string _host = "api.spotify.com";
        private string _port = "443";
        private string _scheme = "https";
        private string _url
        {
            get
            {
                return string.Format("{0}://{1}:{2}/{3}", _scheme, _host, _port, Path);
            }
        }
        private string AccessToken { get; set; }
        public Spotify()
        {

        }
        public Spotify(string scheme, string host, string port)
        {
            _scheme = scheme;
            _host = host;
            _port = port;
        }
        private void SetPath(string path)
        {
            if (string.IsNullOrEmpty(Path))
            {
                Path = path;
            }
        }
    }
}