using System.ComponentModel.DataAnnotations;

namespace Eshop.Core.src.Entity
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; private set; } 

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public BaseEntity()
        {
             Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;
        }
    }
}
