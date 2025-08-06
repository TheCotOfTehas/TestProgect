using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFApp.EntityFrameworkCore;
using ManagementApplication;

namespace ERPSystemDevelopment.Controllers.Warehouse
{
    public class DocumentReceiptsController : Controller
    {
        private readonly ApplicationContext _context;

        public DocumentReceiptsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: DocumentReceipts
        public async Task<IActionResult> Index()
        {
            return View(await _context.DocumentReceipts.ToListAsync());
        }

        // GET: DocumentReceipts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentReceipt = await _context.DocumentReceipts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentReceipt == null)
            {
                return NotFound();
            }

            return View(documentReceipt);
        }

        // GET: DocumentReceipts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocumentReceipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,DateTime")] DocumentReceipt documentReceipt)
        {
            if (ModelState.IsValid)
            {
                documentReceipt.Id = Guid.NewGuid();
                _context.Add(documentReceipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentReceipt);
        }

        // GET: DocumentReceipts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentReceipt = await _context.DocumentReceipts.FindAsync(id);
            if (documentReceipt == null)
            {
                return NotFound();
            }
            return View(documentReceipt);
        }

        // POST: DocumentReceipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Number,DateTime")] DocumentReceipt documentReceipt)
        {
            if (id != documentReceipt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documentReceipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentReceiptExists(documentReceipt.Id))
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
            return View(documentReceipt);
        }

        // GET: DocumentReceipts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentReceipt = await _context.DocumentReceipts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documentReceipt == null)
            {
                return NotFound();
            }

            return View(documentReceipt);
        }

        // POST: DocumentReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var documentReceipt = await _context.DocumentReceipts.FindAsync(id);
            if (documentReceipt != null)
            {
                _context.DocumentReceipts.Remove(documentReceipt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentReceiptExists(Guid id)
        {
            return _context.DocumentReceipts.Any(e => e.Id == id);
        }
    }
}
