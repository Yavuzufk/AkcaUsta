using AkcaUsta.Repositories.Repository;
using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents.HomepageComponents
{
    public class _ServiceComponentPartial:ViewComponent
    {
        private readonly IServiceDal _serviceDal;

        public _ServiceComponentPartial(IServiceDal serviceDal, IMapper mapper)
        {
            _serviceDal = serviceDal;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var services = await _serviceDal.GetAllServicesWithFeaturesAsync();
            return View(services);
        }
    }
}
