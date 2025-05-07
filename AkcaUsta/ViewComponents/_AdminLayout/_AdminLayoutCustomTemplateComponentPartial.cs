using Microsoft.AspNetCore.Mvc;

namespace Akco_BeyazEsya.ViewComponents._AdminLayout
{
    public class _AdminLayoutCustomTemplateComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
