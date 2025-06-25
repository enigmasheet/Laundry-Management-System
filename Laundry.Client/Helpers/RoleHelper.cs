using System.Security.Claims;

namespace Laundry.Client.Helpers 
{
    public static class RoleHelper
    {
        public static bool IsInAnyRole(ClaimsPrincipal user, params string[] roles)
        {
            if (user == null || !(user.Identity?.IsAuthenticated ?? false))
                return false;

            return roles.Any(role => user.IsInRole(role));
        }

        public static bool IsSuperAdmin(ClaimsPrincipal user) => user.IsInRole("SuperAdmin");
        public static bool IsVendorAdmin(ClaimsPrincipal user) => user.IsInRole("VendorAdmin");
        public static bool IsEmployee(ClaimsPrincipal user) => user.IsInRole("Employee");
        public static bool IsVendor(ClaimsPrincipal user) => user.IsInRole("VendorAdmin") || user.IsInRole("Employee");
        public static bool IsCustomer(ClaimsPrincipal user) => user.IsInRole("Customer");
    }
}
