using System;


namespace Laundry.Shared.DTO
{
    public class VendorInquiryDto
    {
        public int Id { get; set; }

        public int VendorId { get; set; }

        // Optional: Minimal vendor info
        public VendorDto? Vendor { get; set; }

        public Guid CustomerId { get; set; }

        // Optional: Minimal customer info
        public UserDto? Customer { get; set; }

        public string Message { get; set; } = null!;

        public DateTime SentAt { get; set; }

        public bool IsResponded { get; set; }
    }

}
