using EFApp.EntityFrameworkCore;
using ManagementApplication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Resources;

namespace ERPSystemDevelopment.Controllers
{
    public class ResourceController : Controller
    {
        private readonly ApplicationContext _context;

        public ResourceController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Resources = await _context.Resources.ToListAsync();
            return View(Resources);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, string name, StatusTD statusTD)
        {
            Resource resource = await _context.Resources.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }

            resource.Name = name;
            resource.Status = statusTD;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
