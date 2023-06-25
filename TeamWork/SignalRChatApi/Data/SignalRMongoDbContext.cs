using Microsoft.EntityFrameworkCore;
using SignalRChatApi.Models;

namespace SignalRChatApi.Data
{
    public class SignalRMongoDbContext : DbContext
    {
        public SignalRMongoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

    }
}
