namespace Laundry.Api.Models
{
    // Enum to represent the status of an Order, improving type safety and readability
    public enum OrderStatus
    {
        Pending = 0,
        InProgress = 1,
        Completed = 2,
        Cancelled = 3
    }

    public class Order
    {
        // Primary key
        public int Id { get; set; }

        // Foreign key to Customer (User)
        public Guid CustomerId { get; set; }

        // Navigation property to Customer entity
        public User Customer { get; set; } = null!;

        // Foreign key to Vendor
        public int VendorId { get; set; }

        // Navigation property to Vendor entity
        public Vendor Vendor { get; set; } = null!;

        // Timestamp when the order was created (default to UTC now)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Optional pickup and delivery dates for the order
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        // Order status stored as an enum, not a string for better type safety
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        // Collection of related OrderItems (order details)
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Computed property for total amount; this is NOT mapped to database column
        // Use [NotMapped] attribute if using EF Core to explicitly mark this
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public decimal TotalAmount => OrderItems.Sum(item => item.TotalPrice);
    }
}
