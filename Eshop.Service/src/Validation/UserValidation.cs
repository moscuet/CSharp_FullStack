using System.Text.RegularExpressions;
using Eshop.Service.src.DTO;

namespace Eshop.Service.src.Validation
{
    public static class UserValidation
    {
        private static readonly Regex EmailRegex = new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", RegexOptions.Compiled);

        public static void ValidateUserCreateDTO(UserCreateDTO userDTO)
        {
            if (string.IsNullOrWhiteSpace(userDTO.FirstName) || string.IsNullOrWhiteSpace(userDTO.LastName))
            {
                throw new ArgumentException("First name and last name must be provided.");
            }

            if (string.IsNullOrWhiteSpace(userDTO.Email) || !IsValidEmail(userDTO.Email))
            {
                throw new ArgumentException("A valid email must be provided.");
            }

            if (string.IsNullOrWhiteSpace(userDTO.Password) || userDTO.Password.Length < 6)
            {
                throw new ArgumentException("Password must be at least 6 characters long.");
            }
        }

        public static void ValidateUserUpdateDTO(UserUpdateDTO userDTO)
        {
            if (userDTO.Password != null && userDTO.Password.Length < 6)
            {
                throw new ArgumentException("Password must be at least 6 characters long.");
            }
        }

        private static bool IsValidEmail(string email)
        {
            return EmailRegex.IsMatch(email);
        }
    }
}
