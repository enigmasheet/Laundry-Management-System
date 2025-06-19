namespace Laundry.Shared.DTO
{
    public class VendorDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Address { get; set; } = null!;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool IsActive { get; set; }

        public double AverageRating { get; set; }

        public int TotalReviews { get; set; }

        // Navigation properties as DTO collections (optional, depending on API needs)
        public List<UserDto>? Users { get; set; }

        public List<ServiceDto>? Services { get; set; }

        public List<OrderDto>? Orders { get; set; }

        public List<VendorInquiryDto>? Inquiries { get; set; }

        public List<ReviewDto>? Reviews { get; set; }
    }
}
