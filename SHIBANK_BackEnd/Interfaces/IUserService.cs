using SHIBANK.Dto;
using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        bool UserExists(int id);
        bool UserExists(string userName);

        User GetUser(int id);
        User GetUser(string userName);

        Task<bool> RegisterUser(User user,string password);
        Task<bool> ChangePassword(User user, string oldPassword,string newPassword);

        bool UpdateUser(User user,UserUpdateDto userUpdateDto);
        bool DeleteUser(User user);


    }
}
