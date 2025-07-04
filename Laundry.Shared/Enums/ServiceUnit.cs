using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.Enums
{

    public enum ServiceUnit
    {
        [Display(Name = "Per Kilogram")]
        PerKg = 1,

        [Display(Name = "Per Item")]
        PerItem = 2,

        [Display(Name = "Per Pair")]
        PerPair = 3,

        [Display(Name = "Per Hour")]
        PerHour = 4,

        [Display(Name = "Per Meter")]
        PerMeter = 5,

        [Display(Name = "Per Blanket")]
        PerBlanket = 6,

        [Display(Name = "Per Shoe")]
        PerShoe = 7
    }

}
