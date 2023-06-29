using SignalRChatApi.Models;
using SignalRChatApi.Repositories;

namespace SignalRChatApi.Services
{
    public class SendMessageService : ISendMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public SendMessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public bool SendMessage(Message message)
        {
            var sender = _userRepository.GetUserById(message.SenderId);
            var receiver = _userRepository.GetUserById(message.ReceiverId);

            if (sender != null && receiver != null)
            {
                _messageRepository.SaveMessage(message);
                return true;
            }
            return false;
        }
    }
}
