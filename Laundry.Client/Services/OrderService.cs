using Laundry.Client.Services.Interface;
using Laundry.Shared.DTOs;

namespace Laundry.Client.Services
{
    public class OrderService : IOrderService
    {
        public async Task<OrderDto> CreateAsync(OrderDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDto?> GetAllByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(int id, OrderDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
