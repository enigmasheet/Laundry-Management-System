using System.ComponentModel.DataAnnotations;

namespace Laundry.Shared.Enums
{
    public enum EmployeeRole
    {
        [Display(Name = "Admin")]
        Admin = 0,

        [Display(Name = "Attendant")]
        Attendant = 1,

        [Display(Name = "Delivery Staff")]
        DeliveryStaff = 2,

        [Display(Name = "Machine Operator")]
        MachineOperator = 3,

        [Display(Name = "Driver")]
        Driver = 4
    }
}
