using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Core.src.ValueObject;




namespace Eshop.Core.src.Entity
{
    [Table("ProductSizes")]
    public class ProductSize : BaseEntity
    {
        [Required]
        public string Value { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}

