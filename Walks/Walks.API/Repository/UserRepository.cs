using Walks.API.Models.DTO;

namespace Walks.API.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<UserDto> Login(LoginDto loginDto)
        {
            
        }

        public Task<UserDto> Register(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
