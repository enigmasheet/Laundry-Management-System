using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.Enums
{
    public enum PickupTypeEnum
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "Home Pickup")]
        HomePickup = 1,

        [Display(Name = "Drop Off")]
        DropOff = 2,

        
    }
    public enum DeliveryTypeEnum
    {
        [Display(Name = "None")]
        None = 0,

        [Display(Name = "Home Delivery")]
        HomeDelivery = 1,

        [Display(Name = "Store Collect")]
        StoreCollect = 2
    }
}
