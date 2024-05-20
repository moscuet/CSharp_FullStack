using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Core.src.Entity
{
    [Table("categories")]
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public string ImageUrl { get; set; }
        
        [ForeignKey("ParentCategoryId")]
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();
        public virtual ICollection<ProductLine> ProductLines { get; set; } = new List<ProductLine>();
    }
}
