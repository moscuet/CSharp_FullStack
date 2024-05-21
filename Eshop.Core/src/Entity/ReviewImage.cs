using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eshop.Core.src.Entity
{
    [Table("review_images")]
    public class ReviewImage : BaseEntity
    {
        public Guid ReviewId { get; set; }

        public string Url { get; set; }

        [JsonIgnore]
        [ForeignKey("ReviewId")]
        public virtual Review Review { get; set; }
    }
}
