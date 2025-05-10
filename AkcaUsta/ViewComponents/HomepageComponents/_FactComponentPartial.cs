using AkcaUsta.Dtos.BuisnessDataDtos;
using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents.HomepageComponents
{
    public class _FactComponentPartial:ViewComponent
    {
        private readonly IBuisnessDataDAl _buisnessDataDAl;
        private readonly IMapper _mapper;

        public _FactComponentPartial(IBuisnessDataDAl buisnessDataDAl, IMapper mapper)
        {
            _buisnessDataDAl = buisnessDataDAl;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var buisnessData = (await _buisnessDataDAl.GetAllAsync()).FirstOrDefault();
            var result = _mapper.Map<ResultBuisnessDataDto>(buisnessData);
            return View(result);
        }
    }
}
