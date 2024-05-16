using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Eshop.Service.src.ServiceAbstraction;
namespace Eshop.WebAPI.src.Service
{
    public class PasswordService : IPasswordService
    {
        private const int SaltSize = 32;
        private const int IterationCount = 20000;
        private const int HashSize = 256 / 8;

        public string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(SaltSize);
            string hashed = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: IterationCount,
                    numBytesRequested: HashSize
                )
            );
            return hashed;
        }

        public bool VerifyPassword(string password, string passwordHash, byte[] salt)
        {
            string hashed = Convert.ToBase64String(
                 KeyDerivation.Pbkdf2(
                     password: password,
                     salt: salt,
                     prf: KeyDerivationPrf.HMACSHA256,
                     iterationCount: IterationCount,
                     numBytesRequested: HashSize
                 )
             );
            return hashed == passwordHash;
        }
    }
}