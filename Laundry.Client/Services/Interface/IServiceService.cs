using Laundry.Shared.DTOs;

namespace Laundry.Client.Services.Interface
{
    public interface IServiceService: IBase<ServiceDto, int>
    {
        Task<List<ServiceDto>> GetByVendorIdAsync(int vendorId);

    }
}
