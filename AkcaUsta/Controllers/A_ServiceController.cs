using AkcaUsta.Dtos.AboutDtos;
using AkcaUsta.Dtos.ServiceDtos;
using AkcaUsta.Entity;
using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.Controllers
{
    public class A_ServiceController : Controller
    {
        private readonly IServiceDal _serviceDal;
        private readonly IMapper _mapper;

        public A_ServiceController(IServiceDal serviceDal, IMapper mapper)
        {
            _serviceDal = serviceDal;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var service = await _serviceDal.GetAllAsync();
            var result = _mapper.Map<List<ResultServiceDto>>(service);
            return View(result);
        }

        public async Task<IActionResult> CreateService(CreateServiceDto createServiceDto)
        {
            try
            {
                var service = _mapper.Map<Service>(createServiceDto);
                await _serviceDal.AddAsync(service); // API'ye gönderiyorsan burada gönder

                // Kullanıcıya bildirim göstermek için TempData kullanabiliriz
                TempData["NotifyMessage"] = "Hizmet verilerimiz başarıyla eklendi!";
                TempData["NotifyType"] = "success"; // Diğer seçenekler: info, warning, danger
                TempData["NotifyTitle"] = "Ekleme İşlemi Başarılı";  // Başlık burada!
            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Hizmet verileri eklenirken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Ekleme İşleminde Bir Problem Oluştu";  // Başlık burada!
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateService(ResultServiceDto resultServiceDto)
        {
            try
            {
                var service = _mapper.Map<Service>(resultServiceDto);
                await _serviceDal.UpdateAsync(service);

                // Kullanıcıya bildirim göstermek için TempData kullanabiliriz
                TempData["NotifyMessage"] = "Hizmet verilerimiz başarıyla güncellendi!";
                TempData["NotifyType"] = "success"; // Diğer seçenekler: info, warning, danger
                TempData["NotifyTitle"] = "Güncelleme İşlemi Başarılı";  // Başlık burada!
            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Hizmet verileri güncellenirken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Güncelleme İşleminde Bir Problem Oluştu";  // Başlık burada!
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                var service = await _serviceDal.GetByIdAsync(id);
                if (service == null) return NotFound();

                await _serviceDal.DeleteAsync(service);

                // Kullanıcıya bildirim göstermek için TempData kullanabiliriz
                TempData["NotifyMessage"] = "Hizmet verilerimiz başarıyla silindi!";
                TempData["NotifyType"] = "success"; // Diğer seçenekler: info, warning, danger
                TempData["NotifyTitle"] = "Silme İşlemi Başarılı";  // Başlık burada!

            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Hizmet verileri silinirken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Silme İşleminde Bir Problem Oluştu";  // Başlık burada!
            }

            return RedirectToAction("Index");
        }
    }
}
