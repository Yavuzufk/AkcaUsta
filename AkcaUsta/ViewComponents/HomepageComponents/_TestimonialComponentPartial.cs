using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents.HomepageComponents
{
    public class _TestimonialComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
