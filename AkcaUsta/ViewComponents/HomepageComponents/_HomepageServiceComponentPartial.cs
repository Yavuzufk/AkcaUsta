using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents.HomepageComponents
{
    public class _HomepageServiceComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
