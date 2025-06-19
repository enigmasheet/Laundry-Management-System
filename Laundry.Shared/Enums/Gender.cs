using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.Enums
{
    public enum Gender
    {
        [Display(Name = "Unspecified")]
        Unspecified = 0,

        [Display(Name = "Male")]
        Male = 1,

        [Display(Name = "Female")]
        Female = 2,

        [Display(Name = "Other")]
        Other = 3
    }
}
