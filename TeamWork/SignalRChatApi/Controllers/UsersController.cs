using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRChatApi.Data;
using Microsoft.AspNetCore.Authorization;

namespace SignalRChatApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {

        public readonly SignalRAppDbContext _context;

        public UsersController(SignalRAppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var model = _context.Users.ToList();    
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
