using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        bool UserExists(int id);
        bool UserExists(string username);

        User GetUser(int id);
        User GetUser(string username);

        bool RegisterUser(User user);



        
    }
}
