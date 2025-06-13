namespace Laundry.Api.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public double QuantityKg { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => (decimal)QuantityKg * UnitPrice;
    }

}
