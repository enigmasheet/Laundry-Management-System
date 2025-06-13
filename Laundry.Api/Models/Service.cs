namespace Laundry.Api.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;     // e.g., Wash, Iron, Dry Clean
        public string Description { get; set; } = null!;
        public decimal PricePerKg { get; set; }

        public int VendorId { get; set; }
        public Vendor Vendor { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }

}
