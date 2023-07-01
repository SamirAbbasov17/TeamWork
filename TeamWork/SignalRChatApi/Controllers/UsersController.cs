using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalRChatApi.Data;
using Microsoft.AspNetCore.Authorization;
using SignalRChatApi.Models;

namespace SignalRChatApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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

        [HttpPut]
        public async Task<IActionResult> UserUpdate(int id, string? Username, string? Email, string? Password, string? ConfirmPassword, string? img , bool KeepLoggedIn = true)
        {
            if(Password == null && ConfirmPassword == null )
            {
                User user = _context.Users.FirstOrDefault(x => x.Id == id);

                user.Email = Email ?? user.Email;
                user.Username = Username ?? user.Username;
                user.KeepLoggedIn = KeepLoggedIn;
                user.ImagePath = img ?? user.ImagePath;
                user.Password = user.Password;

                _context.Users.Update(user);
                _context.SaveChanges();
                return Ok();
            }
            else if (Password == ConfirmPassword)
            {
                User user = _context.Users.FirstOrDefault(x => x.Id == id);

                
                user.Email = Email ?? user.Email;
                user.Password = Password ?? user.Password;
                user.Username = Username ?? user.Username;
                user.KeepLoggedIn = KeepLoggedIn;
                user.ImagePath = img ?? user.ImagePath;

                _context.Users.Update(user);
                _context.SaveChanges();
                return Ok();
            }
            else
            {

                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
           User user = _context.Users.FirstOrDefault(x => x.Id == id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok();
        }
    }
}
