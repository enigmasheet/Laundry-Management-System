using System;
using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.DTO
{
    public class VendorInquiryDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int VendorId { get; set; }

        // Optional: Minimal vendor info
        public VendorDto? Vendor { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        // Optional: Minimal customer info
        public UserDto? Customer { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Message can't be longer than 1000 characters.")]
        public string Message { get; set; } = null!;

        [Required]
        public DateTime SentAt { get; set; }

        public bool IsResponded { get; set; }
    }
}
