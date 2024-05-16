using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Eshop.Core.src.Entity;
using Eshop.Core.src.ValueObject;
using Eshop.Service.src.ServiceAbstraction;

namespace Eshop.WebApi.src.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user, TokenType type)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserRole.ToString()),
            };

            var jwtKey = _configuration["Secrets:JwtKey"];

            if (jwtKey is null)
            {
                throw new ArgumentNullException(
                    "Jwtkey is not found. Check if you have it in appsettings.json"
                );
            }

            var securityKey = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                SecurityAlgorithms.HmacSha256Signature
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            var expires = (type is TokenType.AccessToken) ? DateTime.UtcNow.AddMinutes(30) : DateTime.UtcNow.AddDays(2);

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = securityKey,
                Issuer = _configuration["Secrets:Issuer"],
            };
            var token = tokenHandler.CreateToken(tokenDecriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}