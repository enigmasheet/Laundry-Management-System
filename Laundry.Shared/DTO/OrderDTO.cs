using Laundry.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace Laundry.Shared.DTO
{
    public class OrderDto
    {
        [Required]
        public int Id { get; init; }

        [Required]
        public Guid CustomerId { get; init; }

        public UserDto? Customer { get; init; }

        [Required]
        public int VendorId { get; init; }

        public VendorDto? Vendor { get; init; }

        [Required]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; init; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Pickup Date")]
        public DateTime? PickupDate { get; init; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Delivery Date")]
        public DateTime? DeliveryDate { get; init; }

        [Required]
        public OrderStatus Status { get; init; }

        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item.")]
        public IReadOnlyList<OrderItemDto> OrderItems { get; init; } = Array.Empty<OrderItemDto>();

        [JsonIgnore]
        public decimal TotalAmount => OrderItems.Sum(item => item.TotalPrice);

        public OrderDto(int id, Guid customerId, int vendorId, DateTime createdAt, OrderStatus status, IReadOnlyList<OrderItemDto>? orderItems = null, UserDto? customer = null, VendorDto? vendor = null, DateTime? pickupDate = null, DateTime? deliveryDate = null)
        {
            Id = id;
            CustomerId = customerId;
            VendorId = vendorId;
            CreatedAt = createdAt;
            Status = status;
            OrderItems = orderItems ?? Array.Empty<OrderItemDto>();
            Customer = customer;
            Vendor = vendor;
            PickupDate = pickupDate;
            DeliveryDate = deliveryDate;
        }
    }
}
