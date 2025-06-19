using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.Shared.DTO
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public Guid CustomerId { get; set; }

        // Optionally include customer info
        public UserDto? Customer { get; set; }

        public int? VendorId { get; set; }

        // Optionally include vendor info
        public VendorDto? Vendor { get; set; }

        public int? ServiceId { get; set; }

        // Optionally include service info
        public ServiceDto? Service { get; set; }

        public int Rating { get; set; } // 1 to 5

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
