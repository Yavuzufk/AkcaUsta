using AkcaUsta.Dtos.AboutDtos;
using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents.HomepageComponents
{
    public class _AboutComponentPartial:ViewComponent
    {
        private readonly IAboutDal _aboutDal;
        private readonly IMapper _mapper;

        public _AboutComponentPartial(IAboutDal aboutDal, IMapper mapper)
        {
            _aboutDal = aboutDal;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var about = (await _aboutDal.GetAllAsync()).FirstOrDefault();
            var result = _mapper.Map<ResultAboutDto>(about);
            return View(result);
        }
    }
}
