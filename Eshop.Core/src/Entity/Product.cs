using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        [ForeignKey("ProductLineId")]
        public virtual ProductLine ProductLine { get; set; }
        [JsonIgnore]
        [ForeignKey("ProductSizeId")]
        public virtual ProductSize ProductSize { get; set; }
        [JsonIgnore]
        [ForeignKey("ProductColorId")]
        public virtual ProductColor ProductColor { get; set; }

        // Transient properties for demonstration purposes
        [NotMapped]
        public string ProductLineName => ProductLine?.Title;

        [NotMapped]
        public string ProductSizeValue => ProductSize?.Value;

        [NotMapped]
        public string ProductColorValue => ProductColor?.Value;
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
