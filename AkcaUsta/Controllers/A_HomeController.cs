using AkcaUsta.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class A_HomeController : Controller
    {
        private readonly IServiceRandevuDal _serviceRandevuDal;

        public A_HomeController(IServiceRandevuDal serviceRandevuDal)
        {
            _serviceRandevuDal = serviceRandevuDal;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PasiveCount = await _serviceRandevuDal.GetPendingRandevuCountAsync();
            return View();
        }


    }
}
