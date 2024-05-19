using CIoTDSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CIoTDSystem.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Port=5464;Host=localhost;Database=ciotdsystem-db;Username=postgres;Password=postgres");
    }
}
