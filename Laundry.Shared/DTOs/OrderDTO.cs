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
        [Display(Name = "Pickup Type")]
        public PickupTypeEnum PickupType { get; set; } = PickupTypeEnum.None;

        [Required]
        [Display(Name = "Delivery Type")]
        public DeliveryTypeEnum DeliveryType { get; set; } = DeliveryTypeEnum.None;

        public string OrderCode => $"{VendorId}-{Id}";

        [Required]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Pickup Date")]
        public DateTime? PickupDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Drop-off Date")]
        public DateTime? DropOffDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Delivery Date")]
        public DateTime? DeliveryDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Expected Ready Date")]
        public DateTime? ExpectedReadyDate { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.NotSpecified;

        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item.")]
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

        [MaxLength(500)]
        public string? SpecialInstructions { get; set; }

        public bool IsExpress { get; set; } = false;
        public decimal ExpressCharge { get; set; }

        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal FinalAmount => TotalAmount + ExpressCharge + TaxAmount - DiscountAmount;

        public bool IsPaid => PaymentStatus == PaymentStatus.Paid;

        public bool IsCancelled { get; set; } = false;
        public DateTime? CancelledAt { get; set; }

        public string? CancelReason { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validate date logic based on pickup and delivery types

            // For example, if PickupType is DropOff, PickupDate might be null, and DropOffDate is used instead
            if (PickupType == PickupTypeEnum.DropOff)
            {
                if (!DropOffDate.HasValue)
                {
                    yield return new ValidationResult(
                        "Drop-off Date is required for Drop Off pickup type.",
                        new[] { nameof(DropOffDate) });
                }
            }
            else
            {
                if (!PickupDate.HasValue)
                {
                    yield return new ValidationResult(
                        "Pickup Date is required for the selected Pickup Type.",
                        new[] { nameof(PickupDate) });
                }
            }

            // DeliveryDate validation (required if delivery type other than None)
            if (DeliveryType != DeliveryTypeEnum.None && !DeliveryDate.HasValue)
            {
                yield return new ValidationResult(
                    "Delivery Date is required for the selected Delivery Type.",
                    new[] { nameof(DeliveryDate) });
            }

            // Date order validations (if all relevant dates present)
            if (PickupDate.HasValue && DropOffDate.HasValue && DropOffDate < PickupDate)
            {
                yield return new ValidationResult(
                    "Drop-off Date cannot be earlier than Pickup Date.",
                    new[] { nameof(DropOffDate), nameof(PickupDate) });
            }

            if (PickupDate.HasValue && DeliveryDate.HasValue && DeliveryDate < PickupDate)
            {
                yield return new ValidationResult(
                    "Delivery Date cannot be earlier than Pickup Date.",
                    new[] { nameof(DeliveryDate), nameof(PickupDate) });
            }

            if (DropOffDate.HasValue && DeliveryDate.HasValue && DeliveryDate < DropOffDate)
            {
                yield return new ValidationResult(
                    "Delivery Date cannot be earlier than Drop-off Date.",
                    new[] { nameof(DeliveryDate), nameof(DropOffDate) });
            }

            if (IsCancelled && string.IsNullOrWhiteSpace(CancelReason))
            {
                yield return new ValidationResult(
                    "Cancel reason must be provided if the order is cancelled.",
                    new[] { nameof(CancelReason) });
            }
        }
    }
}
