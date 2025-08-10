using Microsoft.EntityFrameworkCore;
using System;
using ManagementApplication;
using ManagementApplication.BaseEntity;
using ManagementApplication.Document;

namespace EFApp.EntityFrameworkCore
{
    public partial class ApplicationContext : DbContext
    {
        public DbSet<Resource> Resources { get; set; } = null!;
        public DbSet<UnitMeasurement> UnitMeasurements { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Balance> Balances { get; set; } = null!;
        public DbSet<DocumentReceipt> DocumentReceipts { get; set; } = null!;
        public DbSet<ResourceReceipt> ResourcesReceipts { get; set; } = null!;
        public DbSet<DocumentShipment> DocumentShipments { get; set; } = null!;
        public DbSet<ResourceShipment> ResourceShipments { get; set; } = null!;



        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
            SeedData(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=testdb;Trusted_Connection=True;");
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Заполнение таблицы UnitMeasurements
            modelBuilder.Entity<UnitMeasurement>().HasData(
                new UnitMeasurement { Id = Guid.NewGuid(), Name = "Kilogram", Status = StatusTD.Success },
                new UnitMeasurement { Id = Guid.NewGuid(), Name = "Liter", Status = StatusTD.Success },
                new UnitMeasurement { Id = Guid.NewGuid(), Name = "Meter", Status = StatusTD.Success }
            );

            // Заполнение таблицы Resources
            modelBuilder.Entity<Resource>().HasData(
                new Resource { Id = Guid.NewGuid(), Name = "Resource 1", Status = StatusTD.Success },
                new Resource { Id = Guid.NewGuid(), Name = "Resource 2", Status = StatusTD.Success },
                new Resource { Id = Guid.NewGuid(), Name = "Resource 3", Status = StatusTD.Success }
            );

            // Заполнение таблицы Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = Guid.NewGuid(), Name = "Customer A", Status = StatusTD.Success, AddressCustomer = "Тест1" },
                new Customer { Id = Guid.NewGuid(), Name = "Customer B", Status = StatusTD.Success, AddressCustomer = "Тест2" },
                new Customer { Id = Guid.NewGuid(), Name = "Customer C", Status = StatusTD.Success, AddressCustomer = "Тест3" }
            );
        }
    }
}
