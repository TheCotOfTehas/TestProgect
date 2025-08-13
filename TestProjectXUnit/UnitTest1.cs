using EFApp;
using EFApp.EntityFrameworkCore;
using ManagementApplication;
using ManagementApplication.BaseEntity;
using ManagementApplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace TestProjectXUnit
{
    public class UnitTest1
    {
        //переед проверкой отключить автозаполнение начальными данными  SeedData в ApplicationContext

        [Fact]
        public void CannotCreateIf_AlreadyExists()
        {

        }

        private ApplicationContext GetInMemoryContext()
        {
            var dbName = "TestDb_" + Guid.NewGuid().ToString("N"); // Уникальное имя базы для каждого теста
            var connectionString = $"Server=ms-sql-10.in-solve.ru;Database={dbName};User ID=1gb_grand-smeta-kostoma;Password=dfs$t55FD;Encrypt=True;TrustServerCertificate=False;";

            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new ApplicationContext(options);

            // Создаем базу с нужной схемой
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        [Fact]
        public void Create_ShouldAddEntity()
        {
            using var context = GetInMemoryContext();
            var service = new BaseEntityService<Customer>(context);

            var item = service.Create("TestName", StatusTD.Active);
            item.AddressCustomer = "ХАНТЫМАНСИЙСК";

            Assert.NotNull(item);
            Assert.Equal("TestName", item.Name);
            Assert.Equal(StatusTD.Active, item.Status);
            Assert.Equal("ХАНТЫМАНСИЙСК", item.AddressCustomer);
            Assert.NotEqual(Guid.Empty, item.Id);
            Assert.Single(context.Set<Customer>());
        }
    }

}
