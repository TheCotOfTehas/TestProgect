using Microsoft.AspNetCore.Mvc;
using ManagementApplication.BaseEntity;
using ManagementApplication.Interfaces;
using EFApp;

namespace ERPSystemDevelopment.Controllers.ReferenceBooks
{
    public class ResourcesController : Controller
    {
        private readonly IBaseEntityService<Resource> _baseEntityService;

        public ResourcesController(BaseEntityService<Resource> baseEntityService)
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
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] Resource resource)
        {
            _baseEntityService.Create(resource);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Resource resource)
        {
            var existingResource = _baseEntityService.Edit(resource);
            if (!existingResource) return NotFound();
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
