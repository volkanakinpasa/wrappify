namespace wrappify.Interfaces
{
    public interface IRequestConfiguration
    {
        string Host { get; set; }
        string Port { get; set; }
        string Scheme { get; set; }
    }
}