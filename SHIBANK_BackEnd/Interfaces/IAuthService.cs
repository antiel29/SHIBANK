using SHIBANK.Models;

namespace SHIBANK.Interfaces
{
    public interface IAuthService
    {
        User Authenticate(string username, string password);

        string GenerateToken(User user);

    }
}
