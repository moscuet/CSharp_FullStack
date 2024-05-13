using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Core.src.Common;

namespace Eshop.Core.src.Entity
{
    [Table("categories")]
    public class Category : BaseEntity
    {
        [Required, MaxLength(128)]
        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }

        [Required, Url]
        public string Image_Url { get; set; } = AppConstants.CATEGORY_DEFAULT_IMAGE;

        [ForeignKey("ParentId")]
        public virtual Category ParentCategory { get; set; }

        // Children collection to handle nested categories
        public List<Category> SubCategories { get; set; } = new List<Category>();

        public virtual List<Product> Products { get; set; } = new List<Product>();

    }
}
