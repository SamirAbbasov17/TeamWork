using SignalRChatApi.Models;

namespace SignalRChatApi.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(int id);
    }
}
