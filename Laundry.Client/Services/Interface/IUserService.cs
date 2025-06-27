using Laundry.Shared.DTOs;

namespace Laundry.Client.Services.Interface
{
    public interface IUserService: IBase<UserDto , int>
    {
        Task<UserDto> GetMyProfile();

    }
}
