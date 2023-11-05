using SHIBANK.Data;
using SHIBANK.Interfaces;
using SHIBANK.Models;
using Microsoft.EntityFrameworkCore;

namespace SHIBANK.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) 
        {
            _context = context;
        }

        public User? GetUser(int id)
        {
            return _context.Users.Where(u=>u.Id == id).FirstOrDefault();
        }

        public User? GetUser(string userName)
        {
            return _context.Users.Where(u => u.NormalizedUserName == userName.ToUpper()).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u=>u.Id == id);
        }
        public bool UserExists(string userName) 
        {
            return _context.Users.Any(u=>u.NormalizedUserName == userName.ToUpper());
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }
    }
}
