using AkcaUsta.Repositories.Repository;
using AkcaUsta.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AkcaUsta.ViewComponents.HomepageComponents
{
    public class _ServiceRandevuComponentPartial:ViewComponent
    {
        private readonly IServiceDal _serviceDal;

        public _ServiceRandevuComponentPartial(IServiceDal serviceDal)
        {
            _serviceDal = serviceDal;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var services = await _serviceDal.GetAllAsync();

            ViewBag.ServiceList = services.Select(s => new SelectListItem
            {
                Text = s.Title,
                Value = s.Title // istersen ID'yi value olarak kullanabilirsin
            }).ToList();

            return View();
        }
    }
}
