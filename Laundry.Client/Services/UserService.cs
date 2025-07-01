using Laundry.Client.Services.Interface;
using Laundry.Shared.DTOs;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Laundry.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;
        public UserService(HttpClient http)
        {
            _http = http;
        }

        public Task<UserDto?> GetAllByIDAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> GetMyProfile()
        {
            try
            {
                return await _http.GetFromJsonAsync<UserDto>("api/auth/me");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching user: {ex.Message}");
                return null;
            }
        }

        Task<UserDto> IBase<UserDto, int>.CreateAsync(UserDto dto)
        {
            throw new NotImplementedException();
        }

        Task IBase<UserDto, int>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<UserDto>> IBase<UserDto, int>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<UserDto?> IBase<UserDto, int>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task IBase<UserDto, int>.UpdateAsync(int id, UserDto dto)
        {
            throw new NotImplementedException();
        }

    }
}
