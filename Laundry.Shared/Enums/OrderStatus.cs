using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Pending")]
        Pending = 0,

        [Display(Name = "In Progress")]
        InProgress = 1,

        [Display(Name = "Completed")]
        Completed = 2,

        [Display(Name = "Cancelled")]
        Cancelled = 3,

        [Display(Name = "On Hold")]
        OnHold = 4,

        [Display(Name = "Ready For Pickup")]
        ReadyForPickup = 5,

        [Display(Name = "Delivered")]
        Delivered = 6,
    }
}
