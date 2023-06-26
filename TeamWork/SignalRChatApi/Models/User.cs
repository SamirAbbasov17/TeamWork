using System.ComponentModel.DataAnnotations;

namespace SignalRChatApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ImagePath{ get; set; }
        public ICollection<User>? Friends{ get; set; }
        public ICollection<Group>? Groups{ get; set; }

        public DateTime LastActive { get; set; }
        public DateTime CreatedAt { get; set; } 

        //public ICollection<Message>? SentMessages { get; set; } // Kullanıcının gönderdiği mesajlar
        //public ICollection<Message>? ReceivedMessages { get; set; } // Kullanıcının aldığı mesajlar

    }
}
