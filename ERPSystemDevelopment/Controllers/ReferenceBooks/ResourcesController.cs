using EFApp.EntityFrameworkCore;
using Humanizer.Localisation;
using ManagementApplication.BaseEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystemDevelopment.Controllers.ReferenceBooks
{
    public class ResourcesController : Controller
    {
        private readonly ApplicationContext _context;

        public ResourcesController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Resources.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                resource.Id = Guid.NewGuid();
                _context.Add(resource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resource);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Resource resource)
        {
            var existingCustomer = await _context.Resources.FindAsync(resource.Id);
            if (existingCustomer == null) return NotFound();
            bool hasChanges = false;

            if (existingCustomer.Name != resource.Name || existingCustomer.Status != resource.Status)
            {
                existingCustomer.Name = resource.Name;
                existingCustomer.Status = resource.Status;
                hasChanges = true;
            }

            if (hasChanges)
            {
                try
                {
                    _context.Update(existingCustomer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceExists(resource.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource != null)
            {
                _context.Resources.Remove(resource);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceExists(Guid id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
    }
}
