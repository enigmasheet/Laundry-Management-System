using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.Enums
{
    public enum ServiceCategory
    {
        [Display(Name = "Normal Washing")]
        Normal_Washing = 0,

        [Display(Name = "Dry Cleaning")]
        Dry_Cleaning = 1,

        [Display(Name = "Ironing")]
        Ironing = 2,

        [Display(Name = "Deep Washing")]
        Deep_Washing = 3,

        [Display(Name = "Steam Ironing")]
        Steam_Ironing = 4,

        [Display(Name = "Shoe Cleaning")]
        Shoe_Cleaning = 5,

        [Display(Name = "Carpet Cleaning")]
        Carpet_Cleaning = 6,

        [Display(Name = "Curtain Cleaning")]
        Curtain_Cleaning = 7,

        [Display(Name = "Express Service")]
        Express_Service = 8,

        [Display(Name = "Stain Removal")]
        Stain_Removal = 9,

        [Display(Name = "Premium Fabric Care")]
        Premium_Fabric_Care = 10
    }
}
