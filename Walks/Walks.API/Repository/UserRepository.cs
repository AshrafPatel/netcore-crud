using Walks.API.Extension;
using Walks.API.Models.DTO;

namespace Walks.API.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<UserDto> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> Register(RegisterDto registerDto)
        {
            HashSalt hashSalt = HashSalt.GenerateSaltedHash(256, "password1");

            //Your code here

            cmd.Parameters.AddWithValue("@hash", hashSalt.Hash);
            cmd.Parameters.AddWithValue("@salt", hashSalt.Salt);
        }
    }
}
