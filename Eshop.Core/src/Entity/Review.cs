
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Core.src.Entity;

[Table("reviews")]
public class Review : BaseEntity
{
    public Review() { }

    [Required]
    public Guid UserId { get; private set; }

    [Required]
    public Guid ProductId { get; private set; }

    [Required]
    [MaxLength(1080)]
    public string Comment { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
    public int Rating { get; set; }

    [Required]
    public bool IsAnonymous { get; set; } = false; 

    public List<Image> Images { get; set; } = new List<Image>();

    [ForeignKey("ProductId")]
    public Product product { get; }

    [ForeignKey("UserId")]
    public User User { get; }
 public Review(Guid userId, Guid productId, int rating, string comment, bool isAnonymous)
        {
            UserId = userId;
            ProductId = productId;
            Rating = rating;
            Comment = comment;
            IsAnonymous = isAnonymous;
        }
}
