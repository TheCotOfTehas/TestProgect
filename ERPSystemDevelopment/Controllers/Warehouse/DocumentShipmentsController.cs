using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFApp.EntityFrameworkCore;
using ManagementApplication.Document;

namespace ERPSystemDevelopment.Controllers.Warehouse
{
    public class DocumentShipmentsController : Controller
    {
        private readonly ApplicationContext _context;

        public DocumentShipmentsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: DocumentShipments
        public async Task<IActionResult> Index()
        {
            return View(await _context.DocumentShipments.ToListAsync());
        }

        // GET: DocumentShipments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentShipment = await _context.DocumentShipments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentShipment == null)
            {
                return NotFound();
            }

            return View(documentShipment);
        }

        // GET: DocumentShipments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocumentShipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,DateTime,CustomerID,Status")] DocumentShipment documentShipment)
        {
            if (ModelState.IsValid)
            {
                documentShipment.Id = Guid.NewGuid();
                _context.Add(documentShipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentShipment);
        }

        // GET: DocumentShipments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentShipment = await _context.DocumentShipments.FindAsync(id);
            if (documentShipment == null)
            {
                return NotFound();
            }
            return View(documentShipment);
        }

        // POST: DocumentShipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Number,DateTime,CustomerID,Status")] DocumentShipment documentShipment)
        {
            if (id != documentShipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentShipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentShipmentExists(documentShipment.Id))
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
            return View(documentShipment);
        }

        // GET: DocumentShipments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentShipment = await _context.DocumentShipments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentShipment == null)
            {
                return NotFound();
            }

            return View(documentShipment);
        }

        // POST: DocumentShipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var documentShipment = await _context.DocumentShipments.FindAsync(id);
            if (documentShipment != null)
            {
                _context.DocumentShipments.Remove(documentShipment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentShipmentExists(Guid id)
        {
            return _context.DocumentShipments.Any(e => e.Id == id);
        }
    }
}
