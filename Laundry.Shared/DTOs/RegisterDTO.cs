using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laundry.Shared.DTOs
{
    public class RegisterDto
    {
        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [DisplayName("Phone Number")]
        [RegularExpression(@"^9\d{9}$", ErrorMessage = "Phone number must be 10 digits long.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        [DisplayName("Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        
    }

    public class RegisterResponse
    {
        public string Token { get; set; } = string.Empty;
        public double ExpiresIn { get; set; }
        public UserDto User { get; set; } = new();
    }
}
