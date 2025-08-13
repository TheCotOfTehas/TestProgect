using EFApp;
using EFApp.EntityFrameworkCore;
using ManagementApplication.BaseEntity;
using ManagementApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSystemDevelopment.Controllers.ReferenceBooks
{
    public class UnitMeasurementsController : Controller
    {
        private readonly IBaseEntityService<UnitMeasurement> _baseEntityService;

        public UnitMeasurementsController(BaseEntityService<UnitMeasurement> baseEntityService)
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
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] UnitMeasurement unitMeasurement)
        {
            await _baseEntityService.CreateAsync(unitMeasurement);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UnitMeasurement unitMeasurement)
        {
            var existingUnitMeasurement = await _baseEntityService.EditAsync(unitMeasurement);
            if (!existingUnitMeasurement) return NotFound();
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
