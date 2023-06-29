namespace SignalRChat.Models
{
    public class LoginModel
    {
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool KeepLogedIn { get; set; } = true;
    }
}
