using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.Enums
{
    public enum PaymentMethod
    {
        [Display(Name = "Cash")]
        Cash = 0,

        [Display(Name = "Card")]
        Card = 1,

        [Display(Name = "Mobile Wallet")]
        MobileWallet = 2,

        [Display(Name = "Bank Transfer")]
        BankTransfer = 3,

        [Display(Name = "QR Code")]
        QRCode = 4,

        [Display(Name = "Not Specified")]
        NotSpecified = 5
    }
}
