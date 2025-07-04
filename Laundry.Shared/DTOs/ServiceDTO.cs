using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Laundry.Shared.Enums;

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

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Unit is required.")]
        public string Unit { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vendor ID is required.")]
        public int VendorId { get; set; }

        [JsonIgnore]
        public VendorDto? Vendor { get; set; }

        public IReadOnlyList<ReviewDto>? Reviews { get; set; }
    }
}
