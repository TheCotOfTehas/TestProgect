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

        [Fact]
        public void CannotCreateIf_AlreadyExists()
        {

        }

        private ApplicationContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationContext(options);
        }

        [Fact]
        public void Create_ShouldAddEntity()
        {
            using var context = GetInMemoryContext();
            var service = new BaseEntityService<Customer>(context);

            var item = service.Create("TestName", StatusTD.Active);

            Assert.NotNull(item);
            Assert.Equal("TestName", item.Name);
            Assert.Equal(StatusTD.Active, item.Status);
            Assert.NotEqual(Guid.Empty, item.Id);
            Assert.Single(context.Set<Customer>());
        }
    }

}
