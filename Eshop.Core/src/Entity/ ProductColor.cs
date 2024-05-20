using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Core.src.ValueObject;

namespace Eshop.Core.src.Entity;

[Table("product_colors")]
public class ProductColor : BaseEntity
{
    public string Value { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}