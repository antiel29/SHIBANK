using SHIBANK.Data;
using SHIBANK.Interfaces;
using SHIBANK.Models;

namespace SHIBANK.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) 
        {
            _context = context;
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(p=>p.Id == id).FirstOrDefault();
        }

        public User GetUser(string username)
        {
            return _context.Users.Where(p => p.Username == username).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(p => p.Id).ToList();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(p=>p.Id == id);
        }
    }
}
