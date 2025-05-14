using AkcaUsta.Dtos.AppUserDtos;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents.A_UserRegiser
{
    public class _UserRegisterModalComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new UserRegisterDto();
            return View(model);
        }
    }
}
