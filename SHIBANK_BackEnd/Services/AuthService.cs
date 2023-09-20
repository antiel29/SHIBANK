using Microsoft.IdentityModel.Tokens;
using SHIBANK.Interfaces;
using SHIBANK.Models;

namespace SHIBANK.Services
{
    public class AuthService : IAuthService
    {
        public User Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
