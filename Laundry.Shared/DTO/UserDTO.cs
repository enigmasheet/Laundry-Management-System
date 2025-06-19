using Laundry.Shared.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.DTO
{
    public class UserDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public UserRole Role { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public int? VendorId { get; set; }

        public VendorDto? Vendor { get; set; }

        public List<OrderDto>? Orders { get; set; }

        public List<ReviewDto>? Reviews { get; set; }

        public bool IsActive { get; set; }
    }
}
