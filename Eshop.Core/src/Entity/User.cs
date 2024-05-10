using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Core.ValueObject;

namespace Ecommerce.Core.Entities
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)] 
        public string Avatar { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.User;

        // Default constructor for EF Core
        public User() { }

        // Constructor to initialize a User
        public User(string firstName, string lastName, string avatar, string email, string phoneNumber, string password, UserRole role)
        {
            FirstName = firstName;
            LastName = lastName;
            Avatar = avatar;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Role = role;
        }
    }
}
