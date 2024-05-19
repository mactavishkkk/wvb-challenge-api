using CIoTDSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace CIoTDSystem.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<Command> Commands { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Port=5464;Host=localhost;Database=ciotdsystem-db;Username=postgres;Password=postgres");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasMany(e => e.Commands)
                .WithOne(e => e.Device)
                .HasForeignKey(e => e.DeviceId)
                .IsRequired();
        }
    }
}
