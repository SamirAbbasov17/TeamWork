namespace SignalRChat.Models
{
    public class RegisterModel
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get ; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
