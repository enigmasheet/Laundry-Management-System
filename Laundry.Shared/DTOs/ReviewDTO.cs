using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.DTOs
{
    public class ReviewDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        public UserDto? Customer { get; set; }

        public int? VendorId { get; set; }

        public VendorDto? Vendor { get; set; }

        public int? ServiceId { get; set; }

        public ServiceDto? Service { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [MaxLength(1000)]
        public string? Comment { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
