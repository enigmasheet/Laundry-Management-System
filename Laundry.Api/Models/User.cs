using Laundry.Api.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Role { get; set; } = null!;  // "Customer", "VendorAdmin", "Employee", "SuperAdmin"

    public string? Phone { get; set; }
    public string? Address { get; set; }

    public double? Latitude { get; set; }   // for geolocation
    public double? Longitude { get; set; }

    public int? VendorId { get; set; }      // for VendorAdmin or Employee
    public Vendor? Vendor { get; set; }

    public ICollection<Order>? Orders { get; set; }  // for customers

    public bool IsActive { get; set; } = true;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public ICollection<Review> Reviews { get; set; } = new List<Review>();

}
