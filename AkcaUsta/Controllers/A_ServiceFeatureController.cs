using AkcaUsta.Dtos.ServiceFeatureDtos;
using AkcaUsta.Entity;
using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.Controllers
{
    public class A_ServiceFeatureController : Controller
    {
        private readonly IServiceFeatureDal _serviceFeatureDal;
        private readonly IMapper _mapper;

        public A_ServiceFeatureController(IServiceFeatureDal serviceFeatureDal, IMapper mapper)
        {
            _serviceFeatureDal = serviceFeatureDal;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int id)
        {
            var features = await _serviceFeatureDal.GetFeaturesByServiceID(id);
            var featureDtos = _mapper.Map<List<ResultServiceFeatureDto>>(features);
            ViewBag.ServiceID = id; // Yeni kayıt için lazım olabilir
            return View(featureDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceFeatureDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            try
            {
                var entity = _mapper.Map<ServiceFeature>(dto);
                await _serviceFeatureDal.AddAsync(entity);

                // Kullanıcıya bildirim göstermek için TempData kullanabiliriz
                TempData["NotifyMessage"] = "Hizmet özellikleri başarıyla eklendi!";
                TempData["NotifyType"] = "success"; // Diğer seçenekler: info, warning, danger
                TempData["NotifyTitle"] = "Ekleme İşlemi Başarılı";  // Başlık burada!

            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Hizmet özellikleri eklenirken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Ekleme İşleminde Bir Problem Oluştu";  // Başlık burada!
            }

            return RedirectToAction("Index", new { id = dto.ServiceId });
        }

        [HttpPost]
        public async Task<IActionResult> Update(ResultServiceFeatureDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            try
            {
                var entity = _mapper.Map<ServiceFeature>(dto);
                await _serviceFeatureDal.UpdateAsync(entity);

                // Kullanıcıya bildirim göstermek için TempData kullanabiliriz
                TempData["NotifyMessage"] = "Hizmet özellikleri başarıyla güncellendi!";
                TempData["NotifyType"] = "success"; // Diğer seçenekler: info, warning, danger
                TempData["NotifyTitle"] = "Güncelleme İşlemi Başarılı";  // Başlık burada!
            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Hizmet özellikleri güncellenirken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Güncelleme İşleminde Bir Problem Oluştu";  // Başlık burada!
            }


            return RedirectToAction("Index", new { id = dto.ServiceId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _serviceFeatureDal.GetByIdAsync(id);

            try
            {
                await _serviceFeatureDal.DeleteAsync(entity);

                // Kullanıcıya bildirim göstermek için TempData kullanabiliriz
                TempData["NotifyMessage"] = "Hizmet verilerimiz başarıyla silindi!";
                TempData["NotifyType"] = "success"; // Diğer seçenekler: info, warning, danger
                TempData["NotifyTitle"] = "Silme İşlemi Başarılı";  // Başlık burada!

                return RedirectToAction("Index", new { id = entity.ServiceId });
            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Hizmet verileri silinirken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Silme İşleminde Bir Problem Oluştu";  // Başlık burada!

            }
            return RedirectToAction("Index", new { id = entity.ServiceId });
        }
    }
}
