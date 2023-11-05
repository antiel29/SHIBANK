using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User? GetUser(int id);
        User? GetUser(string userName);
        bool UserExists(int id);
        bool UserExists(string userName);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
