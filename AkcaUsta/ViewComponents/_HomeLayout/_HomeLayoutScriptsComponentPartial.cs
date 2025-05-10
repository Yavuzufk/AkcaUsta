using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents._HomeLayout
{
    public class _HomeLayoutScriptsComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
