using Microsoft.AspNetCore.Mvc;

namespace ERPSystemDevelopment.Controllers
{
    public class DependenciesController : Controller
    {
        private readonly MyService _myService;

        public DependenciesController(MyService myService)
        {
            _myService = myService;
        }

        public IActionResult Index()
        {
            var entities = _myService.GetEntities();
            return View();
        }
    }
}
