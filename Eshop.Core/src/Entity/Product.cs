using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Core.src.Entity
{
    [Table("products")]
    public class Product : BaseEntity
    {
        [Required]
        public Guid ProductLineId { get; set; }

        public Guid? ProductSizeId { get; set; }

        public Guid? ProductColorId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Inventory must not be a negative number")]
        public int Inventory { get; set; }

        [ForeignKey("ProductLineId")]
        public virtual ProductLine ProductLine { get; set; }

        [ForeignKey("ProductSizeId")]
        public virtual ProductSize ProductSize { get; set; }

        [ForeignKey("ProductColorId")]
        public virtual ProductColor ProductColor { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
