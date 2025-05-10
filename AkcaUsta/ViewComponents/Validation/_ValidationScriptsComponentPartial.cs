using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents.Validation
{
    public class _ValidationScriptsComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
