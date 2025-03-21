using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using static EventManager.Api.Jwt.TokenService;

namespace EventManager.Api.Jwt
{
    public class TokenService : ITokenService
    {
        public interface ITokenService
        {
            public string GenerateToken(string username, string role);
        }

        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Retrieve the secret key from configuration and convert it to bytes
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[] { new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, role) }
                ),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
