namespace SignalRChatApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ImagePath{ get; set; }
        public ICollection<Message>? Messages{ get; set; } = new List<Message>();
        public ICollection<User>? Friends{ get; set; }
        public ICollection<Group>? Groups{ get; set; }

    }
}
