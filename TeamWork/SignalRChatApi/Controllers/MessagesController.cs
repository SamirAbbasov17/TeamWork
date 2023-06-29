using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRChatApi.Models;
using SignalRChatApi.Repositories;
using SignalRChatApi.Services;

namespace SignalRChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly ISendMessageService _sendMessageService;

        public MessagesController(ISendMessageService sendMessageService)
        {
            _sendMessageService = sendMessageService;
        }

        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            var result = _sendMessageService.SendMessage(message);
            return result ? Ok() : BadRequest();
        }
    }
}
