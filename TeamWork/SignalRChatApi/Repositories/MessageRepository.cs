using MongoDB.Driver;
using SignalRChatApi.Models;

namespace SignalRChatApi.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoCollection<Message> _messageCollection;

        public MessageRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MongoDBConnection");
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("SignalRChat");
            _messageCollection = database.GetCollection<Message>("messages");
        }

        public void SaveMessage(Message message)
        {
            _messageCollection.InsertOne(message);
        }
    }
}
