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

        public Guid? SizeId { get; set; }

        public Guid? ColorId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Inventory must not be a negative number")]
        public int Inventory { get; set; }

        [ForeignKey("ProductLineId")]
        public virtual ProductLine ProductLine { get; set; }

        [ForeignKey("SizeId")]
        public virtual ProductSize? Size { get; set; }

        [ForeignKey("ColorId")]
        public virtual ProductColor? Color { get; set; }

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
