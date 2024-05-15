using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Eshop.Service.src.ServiceAbstraction;
namespace Eshop.WebAPI.src.Service
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(16);
            string hashed = Convert.ToBase64String(
                KeyDerivation.Pbkdf2
                (
                        password: password!,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 600000,
                        numBytesRequested: 256 / 8
                )
            );
            return hashed;
        }

        public bool VerifyPassword(string password, string passwordHash, byte[] salt)
        {
            string hashed = Convert.ToBase64String(
                 KeyDerivation.Pbkdf2
                 (
                         password: password!,
                         salt: salt,
                         prf: KeyDerivationPrf.HMACSHA256,
                         iterationCount: 600000,
                         numBytesRequested: 256 / 8
                 )
             );
             return hashed == passwordHash;
        }
    }
}