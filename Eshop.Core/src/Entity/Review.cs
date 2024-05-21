
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Eshop.Core.src.Entity;

[Table("reviews")]
public class Review : BaseEntity
{
  [Required]
  public Guid UserId { get; private set; }

  [Required]
  public Guid ProductId { get; private set; }

  [Required, MaxLength(1080)]
  public string Comment { get; set; }

  [Required, Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
  public int Rating { get; set; }

  [Required]
  public bool IsAnonymous { get; set; } = false;

 [JsonIgnore]
  [ForeignKey("ProductId")]
  public virtual Product Product { get; set; }
 
 [JsonIgnore]
  [ForeignKey("UserId")]
  public virtual User User { get; set; }

        public virtual ICollection<ReviewImage> ReviewImages { get; set; } = new List<ReviewImage>();
}

