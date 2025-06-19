using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.Enum
{
    public enum PickupType
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "Home Pickup")]
        HomePickup = 1,

        [Display(Name = "Drop Off")]
        DropOff = 2,

        [Display(Name = "Home Delivery")]
        HomeDelivery = 3,

        [Display(Name = "Store Pickup")]
        StorePickup = 4
    }
}
