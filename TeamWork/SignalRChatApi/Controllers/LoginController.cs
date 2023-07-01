using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SignalRChatApi.Models;
using SignalRChatApi.Data;
using Microsoft.AspNetCore.Authorization;
using SignalRChatApi.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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
        public async Task<IActionResult> UserLogin(UserLoginDto loginDto)
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return BadRequest("Istifadeci login olmusdur");
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("this_is_a_long_secret_key_123456789");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, loginDto.Email),
                        new Claim("OtherProperties", "Example Role")
                    }),
                    //Expires = DateTime.UtcNow.AddHours(1),
                    
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwt = tokenHandler.WriteToken(token);

                return Ok(jwt);
            }

            return BadRequest();
        }



        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto.Password == userRegisterDto.ConfirmPassword)
            {
                User user = new()
                {
                    Email = userRegisterDto.Email,
                    Password = userRegisterDto.Password,
                    Username = userRegisterDto.Username,
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
