using Microsoft.EntityFrameworkCore;
using System;
using ManagementApplication;

namespace EF.EntityFrameworkCore
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Resource> Resources { get; set; } = null!;
        public DbSet<Resource> UnitMeasurement { get; set; } = null!;
        public DbSet<Resource> Customer { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=testdb;Trusted_Connection=True;");
        }
    }
}
