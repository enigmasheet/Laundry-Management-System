using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Laundry.Shared.DTOs
{
    public class OrderItemDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        [Display(Name = "Quantity (Kg)")]
        public double QuantityKg { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be non-negative.")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Total Price")]
        [JsonIgnore] // Optional: exclude from JSON if you prefer to compute client-side
        public decimal TotalPrice => (decimal)QuantityKg * UnitPrice;

        // Optional detailed service info
        public ServiceDto? Service { get; set; }
    }
}
