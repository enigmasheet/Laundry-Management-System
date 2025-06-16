namespace Laundry.Api.DTO
{
    // DTO used to return OrderItem details in responses
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;  // To show service name
        public double QuantityKg { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    // DTO used for creating or updating OrderItems (input)
    public class OrderItemCreateUpdateDto
    {
        public int ServiceId { get; set; }
        public double QuantityKg { get; set; }
    }

    // DTO used to return Order details in responses
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;

        public int VendorId { get; set; }
        public string VendorName { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; } = null!;

        public List<OrderItemDto> OrderItems { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }

    // DTO used for creating or updating an Order (input)
    public class OrderCreateUpdateDto
    {
        public int CustomerId { get; set; }
        public int VendorId { get; set; }

        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Status { get; set; } = "Pending";

        public List<OrderItemCreateUpdateDto> OrderItems { get; set; } = new();
    }

}
