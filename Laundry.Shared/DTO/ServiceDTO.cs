using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.Shared.DTO
{
    public class ServiceDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal PricePerKg { get; set; }

        public int VendorId { get; set; }

        // Optional: Vendor details can be included if needed
        public VendorDto? Vendor { get; set; }

        // Optional: Include reviews summary or list
        public List<ReviewDto>? Reviews { get; set; }
    }
}
