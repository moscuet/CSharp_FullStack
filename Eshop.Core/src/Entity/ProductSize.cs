using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Core.src.Entity
{
    [Table("product_sizes")]
    public class ProductSize : BaseEntity
    {
        [Required]
        public string Value { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}   
