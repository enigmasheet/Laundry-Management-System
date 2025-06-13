namespace Laundry.Api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public User Customer { get; set; } = null!;

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public string Status { get; set; } = "Pending";  // Pending, InProgress, Completed, Cancelled

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal TotalAmount => OrderItems.Sum(item => item.TotalPrice);
    }

}
