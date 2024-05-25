using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Core.src.Entity
{
    [Table("product_lines")]
    public class ProductLine : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        [Required, MaxLength(1080)]
        public string Description { get; set; }

       [Required, Column(TypeName = "decimal(18,2)")]
        [Range(0, 9999999.99, ErrorMessage = "Price must be greater than or equal to 0 and less than or equal to 9999999.99")]
        public decimal Price { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
        public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();
       }

}
