using System;
using System.ComponentModel.DataAnnotations;

namespace Laundry.Api.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
        public User Customer { get; set; } = null!;

        public int? VendorId { get; set; }
        public Vendor? Vendor { get; set; }

        public int? ServiceId { get; set; }
        public Service? Service { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
