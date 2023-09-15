using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
    }
}
