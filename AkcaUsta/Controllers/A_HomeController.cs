using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.Controllers
{
    public class A_HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
