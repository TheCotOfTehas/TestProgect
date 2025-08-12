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
        public async Task<IActionResult> Create([Bind("Id,Name,Status")] UnitMeasurement unitMeasurement)
        {
            _baseEntityService.Create(unitMeasurement);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(UnitMeasurement unitMeasurement)
        {
            var existingUnitMeasurement = _baseEntityService.Edit(unitMeasurement);
            if (!existingUnitMeasurement) return NotFound();
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
