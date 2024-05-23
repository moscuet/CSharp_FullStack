using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eshop.Core.src.Entity
{
    [Table("product_images")]
    public class ProductImage : BaseEntity
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required, MaxLength(2048)]
        public string Url { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
