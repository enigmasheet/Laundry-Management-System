using System.ComponentModel.DataAnnotations;

namespace Laundry.Api.Models
{
    public class User
    {
        // Primary key as GUID
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();

        // Basic user info
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!;  // e.g. Customer, VendorAdmin

        // Optional contact details
        public string? Phone { get; set; }
        public string? Address { get; set; }

        // Location info (optional)
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // Vendor relation (nullable for non-vendor users)
        public int? VendorId { get; set; }
        public Vendor? Vendor { get; set; }

        // Navigation properties
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        // Account status & tokens
        public bool IsActive { get; set; } = true;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
