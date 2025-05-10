using AkcaUsta.Dtos.TechnicianDtos;
using AkcaUsta.Entity;
using AkcaUsta.Repositories.IRepository;
using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.Controllers
{
    public class A_TechnicianController : Controller
    {
        private readonly ITechnicianDal _technicianDal;
        private readonly IMapper _mapper;

        public A_TechnicianController(ITechnicianDal technicianDal, IMapper mapper)
        {
            _technicianDal = technicianDal;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var technician = await _technicianDal.GetAllAsync();
            var technicianDtos = _mapper.Map<List<ResultTechnicianDto>>(technician);
            return View(technicianDtos);
        }

        public async Task<IActionResult> Create(CreateTechnicianDto dto)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    if (dto.ImageUrl != null)
                    {
                        var filePath = Path.Combine("wwwroot/Images/Technician", dto.ImageUrl.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await dto.ImageUrl.CopyToAsync(stream);
                        }
                        dto.Image = dto.ImageUrl.FileName;
                    }

                    var entity = _mapper.Map<Technician>(dto);
                    await _technicianDal.AddAsync(entity);

                    // Kullanıcıya bildirim göstermek için TempData kullanabiliriz
                    TempData["NotifyMessage"] = "Usta/Teknisyen verilerimiz başarıyla eklendi!";
                    TempData["NotifyType"] = "success"; // Diğer seçenekler: info, warning, danger
                    TempData["NotifyTitle"] = "Ekleme İşlemi Başarılı";  // Başlık burada!

                    // Başarıyla ekledikten sonra liste sayfasına yönlendirme yapabilirsiniz
                    return RedirectToAction("Index");

                }
                catch (Exception)
                {
                    TempData["NotifyMessage"] = "Usta/Teknisyen eklenirken bir hata oluştu.";
                    TempData["NotifyType"] = "danger";
                    TempData["NotifyTitle"] = "Ekleme İşleminde Bir Problem Oluştu";  // Başlık burada!
                    // Hata durumunda, formu tekrar render et
                    return View(dto);
                }
            }
            // Hata durumunda, formu tekrar render et
            return View(dto);
        }

        public async Task<IActionResult> Update(int id)
        {
            var entity = await _technicianDal.GetByIdAsync(id);
            var dto = _mapper.Map<ResultTechnicianDto>(entity);

            return View("Update", dto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ResultTechnicianDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", dto);
            }
            try
            {
                // Eski görseli kontrol et ve sil
                if (dto.ImageUrl != null)
                {
                    // Eski görselin yolunu al
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Technician", dto.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath); // Eski görseli sil
                    }

                    // Yeni görseli kaydet
                    var filePath = Path.Combine("wwwroot/Images/Technician", dto.ImageUrl.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await dto.ImageUrl.CopyToAsync(stream);
                    }

                    // Yeni görselin yolunu dto'ya ekle
                    dto.Image = dto.ImageUrl.FileName;
                }

                var tecnician = _mapper.Map<Technician>(dto);
                await _technicianDal.UpdateAsync(tecnician);

                TempData["NotifyMessage"] = "Usta/Teknisyen verilerimiz başarıyla güncellendi!";
                TempData["NotifyType"] = "success"; // Diğer seçenekler: info, warning, danger
                TempData["NotifyTitle"] = "Güncelleme İşlemi Başarılı";  // Başlık burada!

            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Güncelleme sırasında bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Güncelleme İşleminde Bir Problem Oluştu";  // Başlık burada!

            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _technicianDal.GetByIdAsync(id);

            try
            {
                // Eski görseli kontrol et ve sil
                if (entity.Image != null)
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Technician", entity.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                await _technicianDal.DeleteAsync(entity);
                TempData["NotifyMessage"] = "Usta/Teknisyen verilerimiz başarıyla silindi!";
                TempData["NotifyType"] = "success"; 
                TempData["NotifyTitle"] = "Silme İşlemi Başarılı"; 

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Usta/Teknisyen verilerimiz silinirken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Silme İşleminde Bir Problem Oluştu";
            }

            return RedirectToAction("Index");
        }



    }
}
