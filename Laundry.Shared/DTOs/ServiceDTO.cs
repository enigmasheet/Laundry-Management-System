using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.DTOs
{
    public class ServiceDto
    {
        [Required(ErrorMessage = "Service ID is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Service name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price per kg is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price per kg must be non-negative.")]
        public decimal Price { get; set; }

        public string Unit { get; set; } = string.Empty; // Must match enum values
        
        [Required(ErrorMessage = "Vendor ID is required.")]
        public int VendorId { get; set; }
         
        public VendorDto? Vendor { get; set; }

        // Optional: Associated customer reviews
        public IReadOnlyList<ReviewDto>? Reviews { get; set; }
    }
}
