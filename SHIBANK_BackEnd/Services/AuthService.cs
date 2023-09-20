using Microsoft.IdentityModel.Tokens;
using SHIBANK.Interfaces;
using SHIBANK.Helper;
using SHIBANK.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Reflection.Metadata.Ecma335;

namespace SHIBANK.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public AuthResult Authenticate(string username, string password)
        {
            var user = _userRepository.GetUser(username);

            if (user == null || password != user.Password)
            {
                return new AuthResult(false, null);
            }

            var token = GenerateToken(user);

            return new AuthResult(true, token);

        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("WelcomeToTheNHK27");

            var now = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                IssuedAt = now,
                Expires = now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
    }
}
