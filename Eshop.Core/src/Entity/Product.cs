using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Core.src.Entity
{
    [Table("products")]
    public class Product : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(1080)]
        public string Description { get; set; }

        [Required, DecimalPrecision(2), Range(0, 9999999.99, ErrorMessage = "Price must be greater than or equal to 0 and lesss than equal 9999999.99")]
        public decimal Price { get; set; }

        [DecimalPrecision(2), Range(0, int.MaxValue, ErrorMessage = "Inventory must not be negative number")]
        public int Inventory { get; set; }

        // Foreign Key for Category
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        
        // Navigation property 
        public List<Image> Images { get; private set; } = new List<Image>();

        public void AddImage(Image image)
        {
            Images.Add(image);
        }

        public void RemoveImage(Guid imageId)
        {
            var image = Images.FirstOrDefault(i => i.Id == imageId);
            if (image != null)
            {
                Images.Remove(image);
            }
        }
    }
}
