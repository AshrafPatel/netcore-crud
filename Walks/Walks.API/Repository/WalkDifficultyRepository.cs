using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models.Domain;

namespace Walks.API.Repository
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly WalksDbContext _walksDbContext;

        public WalkDifficultyRepository(WalksDbContext walksDbContext)
        {
            _walksDbContext = walksDbContext;
        }
        public async Task<WalkDifficulty> AddAsync(WalkDifficulty difficulty)
        {
            difficulty.Id= Guid.NewGuid();
            await _walksDbContext.AddAsync(difficulty);
            await _walksDbContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkDifficulty = await _walksDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDifficulty == null) { return null; }
            _walksDbContext.WalkDifficulties.Remove(walkDifficulty);
            await _walksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await _walksDbContext.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            var walkDifficultyDom = await _walksDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDifficultyDom == null) { return null; }
            return walkDifficultyDom;
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty difficulty)
        {
            var walkDifficultyDom = await _walksDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDifficultyDom == null) { return null;  }
            walkDifficultyDom.Code = difficulty.Code;
            await _walksDbContext.SaveChangesAsync();
            return walkDifficultyDom;
        }
    }
}
