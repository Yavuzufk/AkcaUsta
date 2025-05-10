using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents._HomeLayout
{
    public class _HomeLayoutHeadComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
