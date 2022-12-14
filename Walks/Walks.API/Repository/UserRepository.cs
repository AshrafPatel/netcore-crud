using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Extension;
using Walks.API.Models.Domain;
using Walks.API.Models.DTO;

namespace Walks.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly WalksDbContext _walksDbContext;

        public UserRepository(WalksDbContext walksDbContext)
        {
            _walksDbContext = walksDbContext;
        }

        public async Task<User> AddAsync(User user)
        {
            user.Id = Guid.NewGuid();
            await _walksDbContext.Users.AddAsync(user);
            await _walksDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteAsync(Guid id)
        {
            var user = await _walksDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) { return null; }
            _walksDbContext.Users.Remove(user);
            await _walksDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _walksDbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await _walksDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) { return null; }
            return user;
        }

        public async Task<User> UpdateAsync(Guid id, User user)
        {
            var existingUser = await _walksDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) { return null; }

            existingUser.UserName = user.UserName;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.PasswordSalt = user.PasswordSalt;
            existingUser.Role = user.Role;

            return existingUser;


        }
    }
}
