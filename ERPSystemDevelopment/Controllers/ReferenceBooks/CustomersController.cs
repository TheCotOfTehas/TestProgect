using Microsoft.AspNetCore.Mvc;
using ManagementApplication.BaseEntity;
using ManagementApplication.Interfaces;
using EFApp;
using System.Threading.Tasks;

namespace ERPSystemDevelopment.Controllers.ReferenceBooks
{
    public class CustomersController : Controller
    {
        private readonly IBaseEntityService<Customer> _baseEntityService;

        public CustomersController(BaseEntityService<Customer> baseEntityService)
        {
            _baseEntityService = baseEntityService;
        }

        public async Task<IActionResult> Index()
        {
            var allEntity = await _baseEntityService.GetAllByNameAsync();
            return View(allEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,AddressCustomer,Status")] Customer customer)
        {
            var newCustomer = await _baseEntityService.CreateAsync(customer);
            newCustomer.AddressCustomer = customer.AddressCustomer;
            await _baseEntityService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            var existingCustomer = await _baseEntityService.EditAsync(customer);
            if (!existingCustomer) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _baseEntityService.ArchiveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}