using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
