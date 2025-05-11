using AkcaUsta.Dtos.ServiceRandevuDtos;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents.A_Randevu
{
    public class _CreateRandevuModalComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var viewModel = new CreateServiceRandevuDto();
            return View(viewModel);
        }
    }
}
