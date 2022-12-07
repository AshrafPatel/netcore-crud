using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models.Domain;

namespace Walks.API.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly WalksDbContext _walksDbContext;

        public WalkRepository(WalksDbContext walksDbContext)
        {
            _walksDbContext = walksDbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id= Guid.NewGuid();
            await _walksDbContext.Walks.AddAsync(walk);
            await _walksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var walk = await _walksDbContext.Walks.Include(x => x.Region).Include(x => x.WalkDifficulty).FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null) { return null; }
            _walksDbContext.Walks.Remove(walk);
            await _walksDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
           return await _walksDbContext.Walks.Include(x => x.Region).Include(x => x.WalkDifficulty).ToListAsync();
        }

        public async Task<Walk> GetByIdAsync(Guid id)
        {
            return await _walksDbContext.Walks.Include(x => x.Region).Include(x => x.WalkDifficulty).FirstOrDefaultAsync(x =>x.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var walkInDb = await _walksDbContext.Walks.Include(x => x.Region).Include(x => x.WalkDifficulty).FirstOrDefaultAsync(x => x.Id == id);
            if (walkInDb == null) { return null; }

            walkInDb.Name = walk.Name;
            walkInDb.Length = walk.Length;
            walkInDb.WalkDifficulty = walk.WalkDifficulty;
            walkInDb.RegionId= walk.RegionId;
            await _walksDbContext.SaveChangesAsync();
            return walkInDb;
        }
    }
}
