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

        // GET: UnitMeasurements
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnitMeasurements.ToListAsync());
        }

        // GET: UnitMeasurements/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitMeasurement = await _context.UnitMeasurements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitMeasurement == null)
            {
                return NotFound();
            }

            return View(unitMeasurement);
        }

        // GET: UnitMeasurements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnitMeasurements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] UnitMeasurement unitMeasurement)
        {
            if (ModelState.IsValid)
            {
                unitMeasurement.Id = Guid.NewGuid();

                if (!_context.UnitMeasurements.Contains(unitMeasurement))
                {
                    _context.Add(unitMeasurement);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(unitMeasurement);
        }

        // GET: UnitMeasurements/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitMeasurement = await _context.UnitMeasurements.FindAsync(id);
            if (unitMeasurement == null)
            {
                return NotFound();
            }
            return View(unitMeasurement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Status")] UnitMeasurement unitMeasurement)
        {
            if (id != unitMeasurement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unitMeasurement);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(unitMeasurement);
        }

        // GET: UnitMeasurements/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitMeasurement = await _context.UnitMeasurements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unitMeasurement == null)
            {
                return NotFound();
            }

            return View(unitMeasurement);
        }

        // POST: UnitMeasurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
