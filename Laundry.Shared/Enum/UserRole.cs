using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.Enum
{
    public enum UserRole
    {
        [Display(Name = "Super Admin")]
        SuperAdmin = 0,

        [Display(Name = "Vendor Admin")]
        VendorAdmin = 1,

        [Display(Name = "Vendor Employee")]
        VendorEmployee = 2,

        [Display(Name = "Customer")]
        Customer = 3
    }
}
