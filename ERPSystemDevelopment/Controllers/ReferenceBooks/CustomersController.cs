using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFApp.EntityFrameworkCore;
using ManagementApplication.BaseEntity;
using ManagementApplication.Interfaces;
using EFApp;

namespace ERPSystemDevelopment.Controllers.ReferenceBooks
{
    public class CustomersController : Controller
    {
        private readonly IBaseEntityService<Customer> _baseEntityService;

        public CustomersController(BaseEntityService<Customer> baseEntityService)
        {
            _baseEntityService = baseEntityService;
        }

        public IActionResult Index()
        {
            var allEntity = _baseEntityService.GetAllByName();
            return View(allEntity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,AddressCustomer,Status")] Customer customer)
        {
            var newCustomer = _baseEntityService.Create(customer);
            newCustomer.AddressCustomer = customer.AddressCustomer;
            _baseEntityService.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            var existingCustomer = _baseEntityService.Edit(customer);
            if (!existingCustomer) return NotFound();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _baseEntityService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}