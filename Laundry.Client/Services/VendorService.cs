using Laundry.Client.Services.Interface;
using System.Net.Http.Json;
using Laundry.Shared.DTOs;

namespace Laundry.Client.Services
{
    public class VendorService : IVendorService
    {
        private readonly HttpClient _http;

        public VendorService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<VendorDto>> GetAllVendorsAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<VendorDto>>("api/vendors");
                return result ?? new List<VendorDto>();
            }
            catch (Exception ex)
            {
                // Log exception or handle it appropriately
                Console.Error.WriteLine($"Error fetching vendors: {ex.Message}");
                return new List<VendorDto>();
            }
        }

        public async Task<VendorDto?> GetVendorByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<VendorDto>($"api/vendors/{id}");
        }

    }

}
