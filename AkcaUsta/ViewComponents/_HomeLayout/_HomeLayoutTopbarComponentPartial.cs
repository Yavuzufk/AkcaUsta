using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents._HomeLayout
{
    public class _HomeLayoutTopbarComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
