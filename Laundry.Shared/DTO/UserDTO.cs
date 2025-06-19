using Laundry.Shared.Enum;
namespace Laundry.Shared.DTO
{
    public class UserDto
    {
        public Guid UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        // Use UserRole enum for strong typing instead of string Role
        public UserRole Role { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public int? VendorId { get; set; }

        // Optional minimal vendor info to avoid deep nesting
        public VendorDto? Vendor { get; set; }

        // Optionally include orders and reviews summaries if needed
        public List<OrderDto>? Orders { get; set; }

        public List<ReviewDto>? Reviews { get; set; }

        public bool IsActive { get; set; }
    }
}
