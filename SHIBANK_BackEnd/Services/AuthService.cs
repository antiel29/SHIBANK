using Microsoft.IdentityModel.Tokens;
using SHIBANK.Interfaces;
using SHIBANK.Helper;
using SHIBANK.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Reflection.Metadata.Ecma335;
<<<<<<< HEAD
=======
using Microsoft.Extensions.Configuration;
>>>>>>> master

namespace SHIBANK.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
<<<<<<< HEAD

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
=======
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;

>>>>>>> master
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
<<<<<<< HEAD
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
=======

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
>>>>>>> master
        }
        
    }
}
