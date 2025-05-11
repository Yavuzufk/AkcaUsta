using AkcaUsta.Dtos.TechnicianDtos;
using AkcaUsta.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents.HomepageComponents
{
    public class _TeamComponentPartial:ViewComponent
    {
        private readonly ITechnicianDal _technicianDal;
        private readonly IMapper _mapper;

        public _TeamComponentPartial(ITechnicianDal technicianDal, IMapper mapper)
        {
            _technicianDal = technicianDal;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var technician = await _technicianDal.GetAllAsync();
            var technicianDtos = _mapper.Map<List<ResultTechnicianDto>>(technician);
            return View(technicianDtos);
        }
    }
}
