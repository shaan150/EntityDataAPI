using Microsoft.EntityFrameworkCore;
using EntityDataAPI.Models;
using System.Collections.Generic;
using EntityDataAPI.Models.Entity;

namespace EntityDataAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Date> Dates { get; set; }
        public DbSet<Name> Names { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Entity)
                .WithMany(e => e.Addresses)
                .HasForeignKey(a => a.EntityID);

            modelBuilder.Entity<Date>()
                .HasOne(d => d.Entity)
                .WithMany(e => e.Dates)
                .HasForeignKey(d => d.EntityId);

            modelBuilder.Entity<Name>()
                .HasOne(n => n.Entity)
                .WithMany(e => e.Names)
                .HasForeignKey(n => n.EntityID);

        }

        public void SeedDatabase()
        {
            DataSeeder.Seed(this);
        }
    }
}
