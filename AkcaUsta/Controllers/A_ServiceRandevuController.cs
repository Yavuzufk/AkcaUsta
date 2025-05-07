using AkcaUsta.Dtos.ServiceRandevuDtos;
using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.Controllers
{
    public class A_ServiceRandevuController : Controller
    {
        private readonly IServiceRandevuDal _serviceRandevuDal;
        private readonly IMapper _mapper;

        public A_ServiceRandevuController(IServiceRandevuDal serviceRandevuDal, IMapper mapper)
        {
            _serviceRandevuDal = serviceRandevuDal;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            //var randevu = await _serviceRandevuDal.GetAllAsync();
            //var result = _mapper.Map<List<ResultServiceRandevuDto>>(randevu);
            return View();
        }
    }
}
