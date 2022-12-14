using Walks.API.Models.Domain;
using Walks.API.Models.DTO;

namespace Walks.API.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(Guid id);
        Task<User> UpdateAsync(Guid id, User difficulty);
        Task<User> DeleteAsync(Guid id);
        Task<User> AddAsync(User difficulty);
    }
}
