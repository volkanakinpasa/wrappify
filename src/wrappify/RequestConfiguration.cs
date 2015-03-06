namespace wrappify
{
    public class RequestConfiguration : IRequestConfiguration
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Scheme { get; set; }
        public string Path { get; set; }
    }
}