using Microsoft.IdentityModel.Tokens;
using SHIBANK.Interfaces;
using SHIBANK.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SHIBANK.Security;
using SHIBANK.Results;

namespace SHIBANK.Services
{
    public class AuthService : IAuthService
    {
        private readonly TokenBlacklist _tokenBlacklist;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration, UserManager<User> userManager, TokenBlacklist tokenBlacklist)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenBlacklist = tokenBlacklist;
        }
        public async Task<AuthResult> AuthenticateAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return new AuthResult(false,string.Empty, "User not found.");
            }

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                return new AuthResult(false,string.Empty, "Incorrect password.");
            }

            var token = GenerateToken(user);

            return new AuthResult(true, token,"Successeful login!");

        }
        public string GenerateToken(User user)
        {

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt!.Key!));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddHours(10),
                signingCredentials: singIn
                );

           return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void AddToBlacklist(string token)
        {
            _tokenBlacklist.AddToBlacklist(token);
        }
        public bool IsTokenBlacklisted(string token)
        {
            return _tokenBlacklist.IsTokenBlacklisted(token);
        }
    }
}
