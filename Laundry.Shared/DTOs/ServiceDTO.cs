using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.DTOs
{
    public class ServiceDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100 characters.")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(1000, ErrorMessage = "Description length can't be more than 1000 characters.")]
        public string Description { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price per kg must be non-negative.")]
        public decimal PricePerKg { get; set; }

        [Required]
        public int VendorId { get; set; }

        // Optional: List of reviews
        public IReadOnlyList<ReviewDto>? Reviews { get; set; }
    }
}
