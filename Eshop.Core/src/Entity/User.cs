using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Core.src.Common;
using Eshop.Core.src.Validation;
using Eshop.Core.src.ValueObject;

namespace Eshop.Core.src.Entity
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Required, MaxLength(255)]
        public string Salt { get; set; }
        [Required]

        public UserRole Role { get; set; } = UserRole.User;

        [Required]
        [InternationalPhoneNumber]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Avatar { get; set; } = AppConstants.AVATAR_DEFAULT_IMAGE;

        // Default constructor for EF Core
        public User() { }

        public User(string firstName, string lastName, string email, string passwordHash, string salt, UserRole role, string phoneNumber, string avatar)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            Salt = salt;
            Role = role;
            PhoneNumber = phoneNumber;
            Avatar = avatar;
        }
    }
}
