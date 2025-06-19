using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.Enums
{
    public enum PaymentStatus
    {
        [Display(Name = "Pending")]
        Pending = 0,

        [Display(Name = "Paid")]
        Paid = 1,

        [Display(Name = "Failed")]
        Failed = 2,

        [Display(Name = "Advance Paid")]
        AdvancePaid = 3,

        [Display(Name = "Refunded")]
        Refunded = 4,

        [Display(Name = "Partially Refunded")]
        PartiallyRefunded = 5,

        [Display(Name = "Cancelled")]
        Cancelled = 6,

        [Display(Name = "On Hold")]
        OnHold = 7,

        [Display(Name = "Overdue")]
        Overdue = 8
    }
}
