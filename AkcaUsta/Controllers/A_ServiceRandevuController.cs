using AkcaUsta.Dtos.ServiceFeatureDtos;
using AkcaUsta.Dtos.ServiceRandevuDtos;
using AkcaUsta.Entity;
using AkcaUsta.Repositories.Repository;
using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AkcaUsta.Controllers
{
    public class A_ServiceRandevuController : Controller
    {
        private readonly IServiceRandevuDal _serviceRandevuDal;
        private readonly IServiceDal _serviceDal;
        private readonly IMapper _mapper;

        public A_ServiceRandevuController(IServiceRandevuDal serviceRandevuDal, IMapper mapper, IServiceDal serviceDal)
        {
            _serviceRandevuDal = serviceRandevuDal;
            _mapper = mapper;
            _serviceDal = serviceDal;
        }

        public async Task<IActionResult> Index()
        {
            var randevu = await _serviceRandevuDal.GetAllAsync();
            var result = _mapper.Map<List<ResultServiceRandevuDto>>(randevu);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceRandevuDto dto)
        {
            if (!ModelState.IsValid)
            {
                var services = await _serviceDal.GetAllAsync();
                ViewBag.ServiceList = services.Select(s => new SelectListItem
                {
                    Text = s.Title,
                    Value = s.Title
                }).ToList();

                return RedirectToAction("Index", "Home");
            }
            try
            {
                var entity = _mapper.Map<ServiceRandevu>(dto);
                await _serviceRandevuDal.AddAsync(entity);

                // Kullanıcıya bildirim göstermek için TempData kullanabiliriz
                TempData["NotifyMessage"] = "Randevu isteiğiniz başarılı bir şekilde oluşturuldu!";
                TempData["NotifyType"] = "success"; // Diğer seçenekler: info, warning, danger
                TempData["NotifyTitle"] = "Randevu İsteği Başarılı";  // Başlık burada!

            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Randevu İsteği oluşturulurken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Randevu İsteğinde Problem Oluştu";  // Başlık burada!
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
