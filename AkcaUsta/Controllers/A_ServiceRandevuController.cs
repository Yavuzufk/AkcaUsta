using AkcaUsta.Dtos.ServiceFeatureDtos;
using AkcaUsta.Dtos.ServiceRandevuDtos;
using AkcaUsta.Entity;
using AkcaUsta.Repositories.IRepository;
using AkcaUsta.Repositories.Repository;
using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Humanizer;
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

            var services = await _serviceDal.GetAllAsync();

            ViewBag.ServiceList = services.Select(s => new SelectListItem
            {
                Text = s.Title,
                Value = s.Title 
            }).ToList();

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

                return Redirect(dto.ReturnUrl ?? "/Home/Index");
            }
            try
            {
                var entity = _mapper.Map<ServiceRandevu>(dto);
                await _serviceRandevuDal.AddAsync(entity);

                // Kullanıcıya bildirim göstermek için TempData kullanabiliriz
                TempData["NotifyMessage"] = "Randevu isteiğiniz başarılı bir şekilde oluşturuldu!";
                TempData["NotifyType"] = "success"; // Diğer seçenekler: info, warning, danger
                TempData["NotifyTitle"] = "Randevu Ekleme İsteği Başarılı";  // Başlık burada!

            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Randevu İsteği oluşturulurken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Randevu Ekleme İsteğinde Problem Oluştu";  // Başlık burada!
            }

            return Redirect(dto.ReturnUrl ?? "/Home/Index");
        }

        public async Task<IActionResult> RandevuStatus(int id)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            try
            {
                await _serviceRandevuDal.ToggleRandevuStatusAsync(id);

                TempData["NotifyMessage"] = "Randevu durumu başarılı bir şekilde değiştirildi!";
                TempData["NotifyType"] = "success";
                TempData["NotifyTitle"] = "Durum Değiştirme İşlemi Başarılı";

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Randevu durumu değiştirilirken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Durum Değiştirme İşleminde Bir Problem Oluştu";
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PasiveRandevu()
        {
            var values = await _serviceRandevuDal.GetPassiveRandevuAsync();
            var result = _mapper.Map<List<ResultServiceRandevuDto>>(values);

            return View(result);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _serviceRandevuDal.GetByIdAsync(id);

            try
            {
                await _serviceRandevuDal.DeleteAsync(entity);

                TempData["NotifyMessage"] = "Randevu İsteği başarıyla silindi!";
                TempData["NotifyType"] = "success";
                TempData["NotifyTitle"] = "Silme İşlemi Başarılı";

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Randevu İsteği silinirken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Silme İşleminde Bir Problem Oluştu";
            }

            return RedirectToAction("Index");
        }


    }
}
