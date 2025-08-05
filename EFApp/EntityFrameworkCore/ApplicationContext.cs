using Microsoft.EntityFrameworkCore;
using System;
using ManagementApplication;

namespace EFApp.EntityFrameworkCore
{
    public partial class ApplicationContext : DbContext
    {
        public DbSet<Resource> Resources { get; set; } = null!;
        public DbSet<UnitMeasurement> UnitMeasurements { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=testdb;Trusted_Connection=True;");
        }
    }
}
