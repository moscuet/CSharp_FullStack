using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eshop.Core.src.Entity
{
    [Table("review_images")]
    public class ReviewImage : BaseEntity
    {
        [Required]
        public Guid ReviewId { get; set; }

        [Required, MaxLength(2048)]
        public string Url { get; set; }

        [ForeignKey("ReviewId")]
        public virtual Review Review { get; set; }

    }
}
