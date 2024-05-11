using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Core.src.Common;

namespace Eshop.Core.src.Entity
{
    [Table("categories")]
    public class Category : BaseEntity
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        [Required]
        [Url]
        public string Image { get; set; } = AppConstants.CATEGORY_DEFAULT_IMAGE;

       [ForeignKey("ParentId")]
        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> Children { get; set; } = new List<Category>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public Category() { }
        public Category(string name, string image)
        {
            Name = name;
            Image = image;
        }
    }
}
