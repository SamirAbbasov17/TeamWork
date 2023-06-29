using SignalRChatApi.Models;

namespace SignalRChatApi.Services
{
    public interface ISendMessageService
    {
        public bool SendMessage(Message message);
    }
}
