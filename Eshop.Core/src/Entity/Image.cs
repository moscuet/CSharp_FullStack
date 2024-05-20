using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Core.src.ValueObject;

namespace Eshop.Core.src.Entity
{
    [Table("images")]
    public class Image : BaseEntity
    {
        [Required]
        public Guid EntityId { get; set; }

        [Required, EnumDataType(typeof(EntityType))]
        public EntityType EntityType { get; set; }

        [Required, MaxLength(2048)]
        public string Url { get; set; }

        public Guid? ProductId { get; set; }
        public Guid? ReviewId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("ReviewId")]
        public Review Review { get; set; }
    }
}
