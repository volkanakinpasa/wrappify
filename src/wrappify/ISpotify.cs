using System.Threading.Tasks;

namespace wrappify
{
    public interface ISpotify
    {
        User GetUser(string userId);
        Task<User> GetMe();
        void Search(string v1Search);
    }
}