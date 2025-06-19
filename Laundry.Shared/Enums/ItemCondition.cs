using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.Enums
{
    public enum ItemCondition
    {
        [Display(Name = "Normal")]
        Normal = 0,

        [Display(Name = "Stained")]
        Stained = 1,

        [Display(Name = "Torn")]
        Torn = 2,

        [Display(Name = "Faded")]
        Faded = 3,

        [Display(Name = "Damaged")]
        Damaged = 4
    }
}
