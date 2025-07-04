using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Laundry.Shared.Enums;

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
        [Display(Name = "Quantity")]
        public double Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be non-negative.")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        // Optional: inferred unit (copied from service)
        public ServiceUnit? Unit { get; set; }

        // Optional detailed service info
        public ServiceDto? Service { get; set; }
    }
}
