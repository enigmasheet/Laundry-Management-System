namespace Laundry.Api.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public User Customer { get; set; } = null!;

        public int? VendorId { get; set; }
        public Vendor? Vendor { get; set; }

        public int? ServiceId { get; set; }
        public Service? Service { get; set; }

        public int Rating { get; set; } // 1 to 5
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
