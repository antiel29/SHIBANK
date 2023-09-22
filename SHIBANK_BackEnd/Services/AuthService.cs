using Microsoft.IdentityModel.Tokens;
using SHIBANK.Interfaces;
using SHIBANK.Helper;
using SHIBANK.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Configuration;

namespace SHIBANK.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;

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

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddHours(23),
                signingCredentials: singIn
                );

           return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}
