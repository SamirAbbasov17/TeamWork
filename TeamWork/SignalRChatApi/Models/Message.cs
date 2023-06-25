using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SignalRChatApi.Models
{
    public class Message
    {
        public Message()
        {
            MessageId = Guid.NewGuid();
            SendDate = DateTime.UtcNow;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid MessageId { get; set; }
        public int UserId { get; set; }
        public string? ImagePath { get; set; }
        public string? Text { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime SendDate { get; set; } 
        public User User { get; set; } = null!;
        
    }
}
