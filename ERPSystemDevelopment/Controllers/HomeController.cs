using Microsoft.AspNetCore.Mvc;

namespace ERPSystemDevelopment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
