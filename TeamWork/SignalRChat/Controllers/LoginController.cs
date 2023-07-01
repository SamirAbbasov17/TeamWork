using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRChat.Models;
using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

using System;

namespace SignalRChat.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult UserLogin()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {

                //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Chat");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginModel loginModel)
        {
            var jsonLogin = JsonConvert.SerializeObject(loginModel);
            StringContent content = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("https://localhost:7202/api/Login/UserLogin", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jwt = await responseMessage.Content.ReadAsStringAsync();

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("this_is_a_long_secret_key_123456789");
                var token = tokenHandler.ReadJwtToken(jwt);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = loginModel.KeepLogedIn
                };
                var claimsIdentity = new ClaimsIdentity(token.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),properties);

                return RedirectToAction("Index", "Chat");
            }

            return View(loginModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var jsonRegister = JsonConvert.SerializeObject(registerModel);
            StringContent content = new StringContent(jsonRegister, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("https://localhost:7202/api/Login/UserRegister", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            else
            {
                return View(registerModel);
            }
        }
    }
}
