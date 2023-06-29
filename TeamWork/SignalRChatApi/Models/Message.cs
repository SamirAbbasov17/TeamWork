using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRChatApi.Models
{
    public class Message
    {
        public Message()
        {
            SendDate = DateTime.UtcNow;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime SendDate { get; set; }

    }
}
