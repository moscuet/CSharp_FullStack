using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Core.src.ValueObject;

namespace Eshop.Core.src.Entity
{
    [Table("orders")]
    public class Order : BaseEntity
    {
        [Required]
        public Guid UserId { get; private set; }

        [Required]
        public Guid AddressId { get; private set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Created;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total must be a non-negative number.")]
        public decimal Total { get; set; } = 0.00m;

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        // Navigation property
        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Order() { }
       
        public Order(Guid userId, Guid addressId, OrderStatus status)
        {
            UserId = userId;
            AddressId = addressId;
            Status = status;
        }

        public void AddItem(OrderItem item)
        {
            if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled)
            {
                throw new InvalidOperationException("Cannot add items to an order that is completed or cancelled.");
            }

            Items.Add(item);
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            Total = Items.Sum(i => i.Price * i.Quantity);
        }
    }
}
