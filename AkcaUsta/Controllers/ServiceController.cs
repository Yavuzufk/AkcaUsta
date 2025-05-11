using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
