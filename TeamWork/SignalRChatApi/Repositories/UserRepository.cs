using SignalRChatApi.Data;
using SignalRChatApi.Models;

namespace SignalRChatApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SignalRAppDbContext _context;

        public UserRepository(SignalRAppDbContext context)
        {
            _context = context;
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            return user;
        }
    }
}
