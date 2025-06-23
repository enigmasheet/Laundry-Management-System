using Laundry.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Laundry.Shared.DTOs
{
    public class OrderDto : IValidatableObject
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Order Id must be positive.")]
        public int Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
        public string? CustomerPhone { get; set; }

        public UserDto? Customer { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Vendor Id must be positive.")]
        public int VendorId { get; set; }

        public VendorDto? Vendor { get; set; }

        [Required]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Pickup Date")]
        public DateTime? PickupDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Delivery Date")]
        public DateTime? DeliveryDate { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item.")]
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

        [JsonIgnore]
        public decimal TotalAmount => OrderItems.Sum(item => item.TotalPrice);

        // Custom validation: DeliveryDate must not be earlier than PickupDate
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PickupDate.HasValue && DeliveryDate.HasValue)
            {
                if (DeliveryDate < PickupDate)
                {
                    yield return new ValidationResult(
                        "Delivery Date cannot be earlier than Pickup Date.",
                        new[] { nameof(DeliveryDate), nameof(PickupDate) });
                }
            }
        }
    }
}
