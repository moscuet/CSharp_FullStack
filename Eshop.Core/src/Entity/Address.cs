using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Core.src.Entity;

namespace Eshop.Core.src.Entity
{
    [Table("addresses")]
    public class Address : BaseEntity
    {
        [Required, ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required, MaxLength(100)]
        public string Street { get; set; }

        [Required, MaxLength(100)]
        public string House { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }

        [Required, MaxLength(20)]
        public string ZipCode { get; set; }

        [Required, MaxLength(50)]
        public string Country { get; set; }

        [Required, Phone, MaxLength(20)]
        public string PhoneNumber { get; set; }

        // Navigation property to the User
        public User User { get; set; }

    }
}
