using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SignalRChatApi.Models;
using SignalRChatApi.Data;
using Microsoft.AspNetCore.Authorization;
using SignalRChatApi.Dtos;

namespace SignalRChatApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly SignalRAppDbContext _context;

        public LoginController(SignalRAppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginDto userLogin)
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return BadRequest("Istifadeci login olmusdur");
            }



            var user = _context.Users.FirstOrDefault(u => u.Email == userLogin.Email && u.Password == userLogin.Password);


            if (user != null)
            {
                //List<Claim> claims = new List<Claim>() {
                //    new Claim(ClaimTypes.NameIdentifier, userLogin.Email),
                //    new Claim("OtherProperties","Example Role")

                //};

                //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                //    CookieAuthenticationDefaults.AuthenticationScheme);

                //AuthenticationProperties properties = new AuthenticationProperties()
                //{

                //    AllowRefresh = true,
                //    IsPersistent = userLogin.KeepLogedIn
                //};

                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                //    new ClaimsPrincipal(claimsIdentity), properties);

                return Ok();
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterDto userRegister)
        {
            if (userRegister.Password == userRegister.ConfirmPassword)
            {
                User user = new()
                {
                    Email = userRegister.Email,
                    Password = userRegister.Password,
                    Username = userRegister.Username,
                    KeepLoggedIn = true,
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
