using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  Eshop.Core.src.Entity
{
    [Table("productLine")]
    public class ProductLine : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, MaxLength(1080)]
        public string Description { get; set; }

        [Required, DecimalPrecision(2), Range(0, 9999999.99, ErrorMessage = "Price must be greater than or equal to 0 and lesss than equal 9999999.99")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        
        public IEnumerable<Image> Images { get; set; } 

    }
}
