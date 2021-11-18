using Microsoft.AspNetCore.Mvc;

namespace Coursework.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
