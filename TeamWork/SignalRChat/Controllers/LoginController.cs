using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRChat.Models;
using System.Net.Http.Json;
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
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> UserLogin(LoginModel loginModel)
        {
            string api = "https://localhost:7202/api/Login/UserLogin";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(api, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Chat");
            }
            else
            {
                // Kayıt başarısız oldu, hata işleyin
                var errorResponse = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", errorResponse);
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
