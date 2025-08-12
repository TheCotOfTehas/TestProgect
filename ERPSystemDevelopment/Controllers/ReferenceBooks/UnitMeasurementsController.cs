using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFApp.EntityFrameworkCore;
using ManagementApplication.BaseEntity;

namespace ERPSystemDevelopment.Controllers.ReferenceBooks
{
    public class UnitMeasurementsController : Controller
    {
        private readonly ApplicationContext _context;

        public UnitMeasurementsController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.UnitMeasurements.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] UnitMeasurement unitMeasurement)
        {
            if (ModelState.IsValid)
            {
                unitMeasurement.Id = Guid.NewGuid();
                _context.Add(unitMeasurement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitMeasurement);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UnitMeasurement unitMeasurement)
        {
            var existingUnitMeasurement = await _context.UnitMeasurements.FindAsync(unitMeasurement.Id);
            if (existingUnitMeasurement == null) return NotFound();
            bool hasChanges = false;

            if (existingUnitMeasurement.Name != unitMeasurement.Name || existingUnitMeasurement.Status != unitMeasurement.Status)
            {
                existingUnitMeasurement.Name = unitMeasurement.Name;
                existingUnitMeasurement.Status = unitMeasurement.Status;
                hasChanges = true;
            }

            if (hasChanges)
            {
                try
                {
                    _context.Update(existingUnitMeasurement);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitMeasurementExists(unitMeasurement.Id))
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

            var unitMeasurement = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitMeasurement == null)
            {
                return NotFound();
            }

            return View(unitMeasurement);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var unitMeasurement = await _context.UnitMeasurements.FindAsync(id);
            if (unitMeasurement != null)
            {
                _context.UnitMeasurements.Remove(unitMeasurement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitMeasurementExists(Guid id)
        {
            return _context.UnitMeasurements.Any(e => e.Id == id);
        }
    }
}
