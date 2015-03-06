namespace wrappify
{
    public interface IRequestConfiguration
    {
        string Host { get; set; }
        string Port { get; set; }
        string Scheme { get; set; }
        string Path { get; set; }
    }
}