using Microsoft.AspNetCore.Mvc;
using AkcaUsta.Dtos.TechnicianDtos;

namespace AkcaUsta.ViewComponents.A_Technician
{
    public class _CreateTechnicianModalComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new CreateTechnicianDto(); // Gerekirse boş ViewModel gönder
            return View(viewModel);
        }
    }
}
