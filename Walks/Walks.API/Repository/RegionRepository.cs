using Microsoft.EntityFrameworkCore;
using Walks.API.Data;
using Walks.API.Models.Domain;

namespace Walks.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly WalksDbContext _walksDbContext;

        public RegionRepository(WalksDbContext walksDbContext)
        {
            _walksDbContext = walksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id= Guid.NewGuid();
            await _walksDbContext.Regions.AddAsync(region);
            await _walksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = _walksDbContext.Regions.FirstOrDefault(x => x.Id==id);
            if (region == null) { return null; }
            _walksDbContext.Regions.Remove(region);
            await _walksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _walksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await _walksDbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _walksDbContext.Regions.FirstOrDefaultAsync(x => x.Id==id);
            if (region == null) { return null; }
            
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long= region.Long;
            existingRegion.Population= region.Population;

            await _walksDbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
