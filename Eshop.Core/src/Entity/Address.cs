using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Core.src.Validation;

namespace Eshop.Core.src.Entity
{
    [Table("addresses")]
    public class Address : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        
        [InternationalPhoneNumber, MaxLength(20)]
        public string PhoneNumber { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
