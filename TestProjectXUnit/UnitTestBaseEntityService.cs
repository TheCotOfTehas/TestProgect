using EFApp;
using EFApp.EntityFrameworkCore;
using ManagementApplication;
using ManagementApplication.BaseEntity;
using ManagementApplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace TestProjectXUnit
{
    public class UnitTestBaseEntityService
    {
        //переед проверкой отключить автозаполнение начальными данными  SeedData в ApplicationContext

        private ApplicationContext GetInMemoryContext()
        {
            var dbName = "TestDb_" + Guid.NewGuid().ToString("N");
            var connectionString = $"Server=ms-sql-10.in-solve.ru;Database={dbName};User ID=1gb_grand-smeta-kostoma;Password=dfs$t55FD;Encrypt=True;TrustServerCertificate=False;";

            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new ApplicationContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        [Fact]
        public async Task Create_ShouldAddEntity()
        {
            using var context = GetInMemoryContext();
            var service = new BaseEntityService<Customer>(context);

            var item = await service.CreateAsync("TestName", StatusTD.Active);
            item.AddressCustomer = "ХАНТЫМАНСИЙСК";

            Assert.NotNull(item);
            Assert.Equal("TestName", item.Name);
            Assert.Equal(StatusTD.Active, item.Status);
            Assert.Equal("ХАНТЫМАНСИЙСК", item.AddressCustomer);
            Assert.NotEqual(Guid.Empty, item.Id);
            Assert.Single(context.Set<Customer>());
        }

        [Fact]
        public async Task Delete_NonExistentEntity()
        {
            using var context = GetInMemoryContext();
            var service = new BaseEntityService<Customer>(context);

            var invalidId = Guid.NewGuid();

            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.ArchiveAsync(invalidId));
        }

        [Fact]
        public async Task Edit_ExistingEntity()
        {
            using var context = GetInMemoryContext();
            var service = new BaseEntityService<Customer>(context);

            var created = await service.CreateAsync("OldName", StatusTD.Active);

            created.Name = "NewName";
            var result = await service.EditAsync(created);

            Assert.True(result);

            var updated = await context.Set<Customer>().FindAsync(created.Id);
            Assert.Equal("NewName", updated.Name);
        }

        [Fact]
        public async Task GetAllByStatus()
        {
            using var context = GetInMemoryContext();
            var service = new BaseEntityService<Customer>(context);

            await service.CreateAsync("Active1", StatusTD.Active);
            await service.CreateAsync("Archived1", StatusTD.Archived);
            await service.CreateAsync("Active2", StatusTD.Active);

            var activeItems = await service.GetAllByStatusAsync(StatusTD.Active);

            Assert.Equal(2, activeItems.Count());
        }
    }

}
