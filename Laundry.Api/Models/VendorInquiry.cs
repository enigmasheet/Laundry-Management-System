using System.ComponentModel.DataAnnotations;

namespace Laundry.Api.Models
{
    public class VendorInquiry
    {
        public int Id { get; set; }

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; } = null!;

        public Guid CustomerId { get; set; }
        public User Customer { get; set; } = null!;

        [Required, MaxLength(1000)]
        public string Message { get; set; } = null!;

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public bool IsResponded { get; set; } = false;
    }
}
