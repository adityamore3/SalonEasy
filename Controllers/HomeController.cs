using Microsoft.AspNetCore.Mvc;

namespace SalonEasy.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
