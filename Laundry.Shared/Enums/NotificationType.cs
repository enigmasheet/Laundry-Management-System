using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.Enums
{
    public enum NotificationType
    {
        [Display(Name = "Order Update")]
        OrderUpdate = 0,

        [Display(Name = "Payment Reminder")]
        PaymentReminder = 1,

        [Display(Name = "Promotional")]
        Promotional = 2,

        [Display(Name = "System Alert")]
        SystemAlert = 3,

        [Display(Name = "Feedback Request")]
        FeedbackRequest = 4
    }
}
