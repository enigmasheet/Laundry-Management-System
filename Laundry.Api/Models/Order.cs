using Laundry.Shared.Enums;

namespace Laundry.Api.Models
{
    public class Order
    {
        public int Id { get; set; }

        public Guid CustomerId { get; set; }
        public User Customer { get; set; } = null!;

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; } = null!;

        public string? CustomerPhone { get; set; }

        public string OrderCode => $"{VendorId}-{Id}";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }  // Add this

        public PickupTypeEnum PickupType { get; set; } = PickupTypeEnum.None;
        public DeliveryTypeEnum DeliveryType { get; set; } = DeliveryTypeEnum.None;

        public DateTime? PickupDate { get; set; }
        public DateTime? DropOffDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ExpectedReadyDate { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.NotSpecified;

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

        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }
}