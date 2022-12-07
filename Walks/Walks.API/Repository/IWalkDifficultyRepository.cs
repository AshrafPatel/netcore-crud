using Walks.API.Models.Domain;

namespace Walks.API.Repository
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        Task<WalkDifficulty> GetAsync(Guid id);
        Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty difficulty);
        Task<WalkDifficulty> DeleteAsync(Guid id);
        Task<WalkDifficulty> AddAsync(WalkDifficulty difficulty);

    }
}
