namespace SignalRChatApi.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = null!;
        public string? GroupImage { get; set; }
        public ICollection<User> Users { get; set; } = null!;


    }
}
