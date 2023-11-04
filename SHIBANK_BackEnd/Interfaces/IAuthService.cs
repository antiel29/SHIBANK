using Microsoft.AspNetCore.Authentication;
using SHIBANK.Models;
using SHIBANK.Results;

namespace SHIBANK.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> AuthenticateAsync(string username, string password);
        string GenerateToken(User user);

        void AddToBlacklist(string token);

        bool IsTokenBlacklisted(string token);
    }
}
