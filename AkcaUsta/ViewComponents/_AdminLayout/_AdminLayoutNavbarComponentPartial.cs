using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Akco_BeyazEsya.ViewComponents._AdminLayout
{
    public class _AdminLayoutNavbarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var name = HttpContext.User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.Name)?.Value ?? "Ziyaretçi";
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "ziyaretci@example.com";
            ViewBag.Name = name;
            ViewBag.Email = email;
            return View();
        }
    }
}
