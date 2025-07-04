using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Laundry.Shared.DTOs
{
    public class VendorDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Name length can't be more than 150 characters.")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(1000, ErrorMessage = "Description length can't be more than 1000 characters.")]
        public string Description { get; set; } = null!;

        [Required]
        [Phone]
        public string Phone { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(250)]
        public string Address { get; set; } = null!;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool IsActive { get; set; }

        [Range(0, 5)]
        public double AverageRating { get; set; }

        public int TotalReviews { get; set; }

        // Optional collections - nullable for cases when they are not loaded
        [JsonIgnore] 
        public List<UserDto>? Users { get; set; }
        [JsonIgnore]
        public List<ServiceDto>? Services { get; set; }
        [JsonIgnore]
        public List<OrderDto>? Orders { get; set; }
        [JsonIgnore]
        public List<VendorInquiryDto>? Inquiries { get; set; }
        [JsonIgnore]
        public List<ReviewDto>? Reviews { get; set; }
    }
}
