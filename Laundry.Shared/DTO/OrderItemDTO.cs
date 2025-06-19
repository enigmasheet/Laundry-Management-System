using Laundry.Shared.Enum;
namespace Laundry.Shared.DTO
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ServiceId { get; set; }

        public double QuantityKg { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => (decimal)QuantityKg * UnitPrice;

        // Optional: Include service details if needed
        public ServiceDto? Service { get; set; }
    }
}
