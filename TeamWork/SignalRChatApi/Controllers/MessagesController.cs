using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRChatApi.Models;
using SignalRChatApi.Repositories;

namespace SignalRChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessagesController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            _messageRepository.SaveMessage(message);
            return Ok();
        }
    }
}
