using Walks.API.Models.DTO;

namespace Walks.API.Repository
{
    public interface IUserRepository
    {
        Task<UserDto> Login(LoginDto loginDto);
        Task<UserDto> Register(RegisterDto registerDto);
    }
}
