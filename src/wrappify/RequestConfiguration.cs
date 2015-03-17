using wrappify.Interfaces;

namespace wrappify
{
    public class RequestConfiguration : IRequestConfiguration
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Scheme { get; set; }
        public RequestConfiguration(string host, string port, string scheme)
        {
            Host = host;
            Port = port;
            Scheme = scheme;
        }
    }
}