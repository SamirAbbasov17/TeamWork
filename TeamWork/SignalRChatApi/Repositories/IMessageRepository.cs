using SignalRChatApi.Models;

namespace SignalRChatApi.Repositories
{
    public interface IMessageRepository
    {
        void SaveMessage(Message message);
    }
}
