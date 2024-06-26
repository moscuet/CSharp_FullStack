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
        [Required, MinLength(2), MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        public byte[] Salt { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public UserRole UserRole { get; set; } = UserRole.User;

        [Required, InternationalPhoneNumber, MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(2048)]
        public string Avatar { get; set; } = AppConstants.AVATAR_DEFAULT_IMAGE;

        public virtual List<Address> Addresses { get; set; } = new List<Address>();


        public void AddAddress(Address address)
        {
            Addresses.Add(address);
        }

        public void RemoveAddress(Guid addressId)
        {
            var address = Addresses.FirstOrDefault(a => a.Id == addressId);
            if (address != null)
            {
                Addresses.Remove(address);
            }
        }
    }
}
