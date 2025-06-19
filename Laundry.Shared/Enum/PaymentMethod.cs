using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.Enum
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
        QRCode = 4
    }
}
