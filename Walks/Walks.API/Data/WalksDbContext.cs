using Microsoft.EntityFrameworkCore;
using Walks.API.Models.Domain;

namespace Walks.API.Data
{
    public class WalksDbContext : DbContext
    {
        public WalksDbContext(DbContextOptions<WalksDbContext> options) : base(options)
        {

        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulties { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
