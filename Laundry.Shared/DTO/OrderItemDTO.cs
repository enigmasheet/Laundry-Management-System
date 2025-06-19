using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Laundry.Shared.DTO
{
    public class OrderItemDto
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public int OrderId { get; init; }

        [Required]
        public int ServiceId { get; init; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        [Display(Name = "Quantity (Kg)")]
        public double QuantityKg { get; init; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be non-negative.")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; init; }

        [Display(Name = "Total Price")]
        [JsonIgnore] // Optional: exclude from JSON if you prefer to compute client-side
        public decimal TotalPrice => (decimal)QuantityKg * UnitPrice;

        // Optional detailed service info
        public ServiceDto? Service { get; init; }
    }
}
