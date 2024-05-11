using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Core.src.Entity
{
    [Table("product_colors")]
    public class ProductColor
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid ColorId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("ColorId")]
        public virtual Color Color { get; set; }
    }
}
