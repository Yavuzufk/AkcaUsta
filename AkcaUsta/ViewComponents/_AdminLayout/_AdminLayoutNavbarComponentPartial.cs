using Microsoft.AspNetCore.Mvc;

namespace Akco_BeyazEsya.ViewComponents._AdminLayout
{
    public class _AdminLayoutNavbarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
