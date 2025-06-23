using Laundry.Shared.Enums;

namespace Laundry.Api.Models
{
    public class Order
    {
        public int Id { get; set; }

        public Guid CustomerId { get; set; }

        public User Customer { get; set; } = null!;

        public int VendorId { get; set; }

        public Vendor Vendor { get; set; } = null!;

        public string? OrderCode { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? PickupDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal TotalAmount { get; set; }
    }
}
