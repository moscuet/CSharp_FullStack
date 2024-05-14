using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Core.src.Entity
{
    [Table("products")]
    public class Product : BaseEntity
    {
        [Required, ForeignKey("ProductLineId")]
        public Guid ProductLineId { get; set; }

        [ForeignKey("SizeId")]
        public Guid? SizeId { get; set; }

        [ForeignKey("ColorId")]
        public Guid? ColorId { get; set; }

        [Required, DecimalPrecision(2), Range(0, int.MaxValue, ErrorMessage = "Inventory must not be negative number")]
        public int Inventory { get; set; }

         public virtual ProductLine ProductLine { get; set; }
        public virtual ProductSize? Size { get; set; }
        public virtual ProductColor? Color { get; set; }
    }
}