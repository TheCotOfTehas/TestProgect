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
        public DbSet<DocumentReceipt> DocumentReceipts { get; set; } = null!;
        public DbSet<DocumentShipment> DocumentShipments { get; set; } = null!;
        public DbSet<Balance> Balances { get; set; } = null!;

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
            OnModelCreatingResourceReceipt(modelBuilder);
            OnModelCreatingResourceShipment(modelBuilder);
        }

        protected void OnModelCreatingResourceReceipt(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Status).IsRequired();

                entity.HasMany<ResourceReceipt>()
                      .WithOne() // Здесь не создаем обратную связь
                      .HasForeignKey(rr => rr.ResourceId);
            });

            modelBuilder.Entity<UnitMeasurement>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Status).IsRequired();

                entity.HasMany<ResourceReceipt>()
                      .WithOne() // Здесь не создаем обратную связь
                      .HasForeignKey(rr => rr.UnitId);
            });

            modelBuilder.Entity<ResourceReceipt>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).IsRequired();

                // Сопоставление с Resource и UnitMeasurement
                entity.HasOne<Resource>()
                      .WithMany(rr => rr.ResourceReceipts)
                      .HasForeignKey(rr => rr.ResourceId);

                entity.HasOne<UnitMeasurement>()
                      .WithMany(rr => rr.ResourceReceipts)
                      .HasForeignKey(rr => rr.UnitId);
            });
        }

        protected void OnModelCreatingResourceShipment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourceShipment>(entity =>
            {
                entity.HasKey(rs => rs.Id);

                entity.HasOne(rs => rs.Resource)
                      .WithMany(r => r.ResourceShipments)
                      .HasForeignKey(rs => rs.ResourceId);

                entity.HasOne(rs => rs.UnitMeasurement)
                      .WithMany(um => um.ResourceShipments) 
                      .HasForeignKey(rs => rs.UnitId);
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=testdb;Trusted_Connection=True;");
        }
    }
}
