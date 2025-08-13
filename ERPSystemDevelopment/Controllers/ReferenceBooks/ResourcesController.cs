using Microsoft.AspNetCore.Mvc;
using ManagementApplication.BaseEntity;
using ManagementApplication.Interfaces;
using EFApp;
using System.Threading.Tasks;

namespace ERPSystemDevelopment.Controllers.ReferenceBooks
{
    public class ResourcesController : Controller
    {
        private readonly IBaseEntityService<Resource> _baseEntityService;

        public ResourcesController(BaseEntityService<Resource> baseEntityService)
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
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] Resource resource)
        {
            await _baseEntityService.CreateAsync(resource);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Resource resource)
        {
            var existingResource = await _baseEntityService.EditAsync(resource);
            if (!existingResource) return NotFound();
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
