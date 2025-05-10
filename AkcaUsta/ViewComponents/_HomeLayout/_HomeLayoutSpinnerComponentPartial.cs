using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents._HomeLayout
{
    public class _HomeLayoutSpinnerComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
