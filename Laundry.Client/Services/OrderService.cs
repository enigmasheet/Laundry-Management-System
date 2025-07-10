using Laundry.Client.Services.Interface;
using Laundry.Shared.DTOs;
using System.Net.Http.Json;

namespace Laundry.Client.Services
{

    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orders", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderDto>() ?? throw new Exception("Failed to deserialize created order.");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/orders/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await _httpClient.GetFromJsonAsync<List<OrderDto>>("api/orders");
            return orders ?? new List<OrderDto>();
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/orders/{id}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<OrderDto>();
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
            else
                response.EnsureSuccessStatusCode();

            return null; // unreachable but required by compiler
        }

        // Just forward this to GetByIdAsync since GetAllByIDAsync seems redundant here
        public Task<OrderDto?> GetAllByIDAsync(int id) => GetByIdAsync(id);

        public async Task UpdateAsync(int id, OrderDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/orders/{id}", dto);
            response.EnsureSuccessStatusCode();
        }
    }

}
