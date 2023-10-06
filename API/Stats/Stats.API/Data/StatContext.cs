using Microsoft.EntityFrameworkCore;
using Stats.API.Models;

namespace Stats.API.Data
{
    public class StatContext : DbContext
    {
        public StatContext(DbContextOptions<StatContext> options) : base(options)
        {
        }

        public DbSet<Platform> Platforms { get; set; }
        public DbSet<SundayData> SundayDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>().ToTable("Platform");
            modelBuilder.Entity<SundayData>().ToTable("SundayData");
        }
    }
}
