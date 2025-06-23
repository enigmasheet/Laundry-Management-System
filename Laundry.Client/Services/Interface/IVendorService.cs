using Laundry.Shared.DTOs;

namespace Laundry.Client.Services.Interface
{
    public interface IVendorService
    {
        Task<List<VendorDto>> GetAllVendorsAsync();
        Task<VendorDto?> GetVendorByIdAsync(int id);

    }
}
