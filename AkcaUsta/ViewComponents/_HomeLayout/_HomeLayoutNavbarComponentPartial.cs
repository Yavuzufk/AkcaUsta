using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents._HomeLayout
{
    public class _HomeLayoutNavbarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
