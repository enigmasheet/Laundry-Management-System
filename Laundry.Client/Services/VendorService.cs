using Laundry.Client.Services.Interface;
using Laundry.Shared.DTOs;
using System.Net.Http.Json;

namespace Laundry.Client.Services
{
    public class VendorService : IVendorService
    {
        private readonly HttpClient _http;

        public VendorService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<VendorDto>> GetAllAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<VendorDto>>("api/vendors");
                return result ?? new List<VendorDto>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching vendors: {ex.Message}");
                return new List<VendorDto>(); 
            }
        }
        public Task<VendorDto?> GetAllByIDAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<VendorDto?> GetByIdAsync(int id)
        {
            try
            {
                return await _http.GetFromJsonAsync<VendorDto>($"api/vendors/{id}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching vendor with ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<VendorDto> CreateAsync(VendorDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/vendors", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<VendorDto>() ?? dto;
        }

        public async Task UpdateAsync(int id, VendorDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/vendors/{id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/vendors/{id}");
            response.EnsureSuccessStatusCode();
        }

       
    }
}
