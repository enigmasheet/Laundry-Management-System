using Laundry.Client.Services.Interface;
using Laundry.Shared.DTOs;
using System.Net.Http.Json;

namespace Laundry.Client.Services
{
    public class ServiceService : IServiceService
    {
        private readonly HttpClient _http;

        public ServiceService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ServiceDto>> GetAllAsync()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<ServiceDto>>("api/services");
                return result ?? new List<ServiceDto>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching services: {ex.Message}");
                return new List<ServiceDto>();
            }
        }

        public async Task<ServiceDto?> GetByIdAsync(int id)
        {
            try
            {
                return await _http.GetFromJsonAsync<ServiceDto>($"api/services/{id}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching service with ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ServiceDto>> GetByVendorIdAsync(int vendorId)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<ServiceDto>>($"api/services/vendor/{vendorId}");
                return result ?? new List<ServiceDto>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching services for vendor {vendorId}: {ex.Message}");
                return new List<ServiceDto>();
            }
        }

        public async Task<ServiceDto> CreateAsync(ServiceDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/services", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ServiceDto>() ?? dto;
        }

        public async Task UpdateAsync(int id, ServiceDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/services/{id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/services/{id}");
            response.EnsureSuccessStatusCode();
        }

        // You can remove this or implement if needed.
        public Task<ServiceDto?> GetAllByIDAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
