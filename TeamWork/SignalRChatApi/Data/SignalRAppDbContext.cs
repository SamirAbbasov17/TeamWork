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
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Message modelini MongoDB'de saklamak için Ignore işlemi yapılır
        //    modelBuilder.Ignore<Message>();

        //    // One-to-many ilişkiyi tanımlama
        //    modelBuilder.Entity<Message>()
        //.HasOne(m => m.Sender)
        //.WithMany(u => u.SentMessages)
        //.HasForeignKey(m => m.SenderId)
        //.OnDelete(DeleteBehavior.Restrict); // Kaskat davranışını Restrict olarak güncelleyin

        //    modelBuilder.Entity<Message>()
        //        .HasOne(m => m.Receiver)
        //        .WithMany(u => u.ReceivedMessages)
        //        .HasForeignKey(m => m.ReceiverId)
        //        .OnDelete(DeleteBehavior.Restrict); // Kaskat davranışını Restrict olarak güncelleyin
        //}
    }
}
