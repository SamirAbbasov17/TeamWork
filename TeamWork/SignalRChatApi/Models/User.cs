using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRChatApi.Models
{
    public class User
    {
        public User()
        {
            CreatedAt = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ImagePath { get; set; }
        public ICollection<Group>? Groups { get; set; }
        public DateTime CreatedAt { get; set; }
        [NotMapped]
        public bool KeepLoggedIn { get; set; }
        public ICollection<Friendship> Friendships { get; set; }

    }
}
