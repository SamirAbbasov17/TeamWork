using Microsoft.EntityFrameworkCore;
using SignalRChatApi.Models;

namespace SignalRChatApi.Data
{
    public class SignalRAppDbContext : DbContext
    {
        public SignalRAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

    }
}
