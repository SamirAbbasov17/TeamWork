using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRChat.Models;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

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
        public IActionResult UserLogin(LoginModel loginModel)
        {
         
            string api = "https://localhost:7202/api/Login/UserLogin";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync(api, jsonContent).Result;


           

            if (response.IsSuccessStatusCode)
            {
                List<Claim> claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, loginModel.Email),
                    new Claim("OtherProperties","Example Role")

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {

                    AllowRefresh = true,
                    IsPersistent = loginModel.KeepLogedIn
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index","Chat");
            }
            else
            {
                // Kayıt başarısız oldu, hata işleyin
                //var errorResponse = response.Content.ReadAsStringAsync();
                //ModelState.AddModelError("", errorResponse);
                return View(loginModel);
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            return View();
        }


    }
}
