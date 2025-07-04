using Laundry.Client.Services.Interface;
using Laundry.Shared.DTOs;
using System.Net.Http.Json;

namespace Laundry.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;
        public UserService(HttpClient http)
        {
            _http = http;
        }


        public async Task<UserDto> GetProfileAsync()
        {
            try
            {
                var response = await _http.GetAsync("api/users/profile");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserDto>();
                }

                Console.Error.WriteLine($"Failed to fetch user profile. Status: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Exception while fetching user profile: {ex.Message}");
                return null;
            }
        }


        public async Task<UserDto?> UpdateProfileAsync(UserDto profile)
        {
            try
            {
                var response = await _http.PutAsJsonAsync("api/users/profile", profile);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserDto>();
                }

                Console.Error.WriteLine($"Failed to update profile. Status: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating profile: {ex.Message}");
                return null;
            }
        }



        //From IBas 
        public Task<UserDto?> GetAllByIDAsync(int id)
        {
            throw new NotImplementedException();
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
