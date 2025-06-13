namespace Laundry.Api.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public bool IsActive { get; set; } = true;
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }


        public ICollection<User> Users { get; set; } = new List<User>();         // Admin, Employees
        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<VendorInquiry> Inquiries { get; set; } = new List<VendorInquiry>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }


}
