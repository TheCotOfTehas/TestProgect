using Microsoft.EntityFrameworkCore;
using System;
using ManagementApplication;
using ManagementApplication.BaseEntity;
using ManagementApplication.Document;

namespace EFApp.EntityFrameworkCore
{
    public partial class ApplicationContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Resource> Resources { get; set; } = null!;
        public DbSet<UnitMeasurement> UnitMeasurements { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Balance> Balances { get; set; } = null!;
        public DbSet<DocumentReceipt> DocumentReceipts { get; set; } = null!;
        public DbSet<ResourceReceipt> ResourcesReceipts { get; set; } = null!;
        public DbSet<DocumentShipment> DocumentShipments { get; set; } = null!;
        public DbSet<ResourceShipment> ResourceShipments { get; set; } = null!;

        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Заполнение таблицы UnitMeasurements
            modelBuilder.Entity<UnitMeasurement>().HasData(
                new UnitMeasurement { Id = Guid.NewGuid(), Name = "Kilogram", Status = StatusTD.Active },
                new UnitMeasurement { Id = Guid.NewGuid(), Name = "Liter", Status = StatusTD.Active },
                new UnitMeasurement { Id = Guid.NewGuid(), Name = "Meter", Status = StatusTD.Active }
            );

            // Заполнение таблицы Resources
            modelBuilder.Entity<Resource>().HasData(
                new Resource { Id = Guid.NewGuid(), Name = "Resource 1", Status = StatusTD.Active },
                new Resource { Id = Guid.NewGuid(), Name = "Resource 2", Status = StatusTD.Active },
                new Resource { Id = Guid.NewGuid(), Name = "Resource 3", Status = StatusTD.Active }
            );

            // Заполнение таблицы Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = Guid.NewGuid(), Name = "Customer A", Status = StatusTD.Active, AddressCustomer = "Тест1" },
                new Customer { Id = Guid.NewGuid(), Name = "Customer B", Status = StatusTD.Active, AddressCustomer = "Тест2" },
                new Customer { Id = Guid.NewGuid(), Name = "Customer C", Status = StatusTD.Active, AddressCustomer = "Тест3" }
            );
        }
    }
}
