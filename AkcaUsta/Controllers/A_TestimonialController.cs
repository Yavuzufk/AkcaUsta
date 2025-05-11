using AkcaUsta.Dtos.TestimonialDtos;
using AkcaUsta.Entity;
using AkcaUsta.Repositories.IRepository;
using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AkcaUsta.Controllers
{
    public class A_TestimonialController : Controller
    {
        private readonly ITestimonialDal _testimonialDal;
        private readonly IServiceDal _serviceDal;
        private readonly IMapper _mapper;

        public A_TestimonialController(ITestimonialDal testimonialDal, IMapper mapper, IServiceDal serviceDal)
        {
            _testimonialDal = testimonialDal;
            _mapper = mapper;
            _serviceDal = serviceDal;
        }

        public async Task<IActionResult> Index()
        {
            var services = await _serviceDal.GetAllAsync();

            ViewBag.ServiceList = services.Select(s => new SelectListItem
            {
                Text = s.Title,
                Value = s.Title 
            }).ToList();

            var testimonial = await _testimonialDal.GetAllAsync();
            var dto = _mapper.Map<List<ResultTestimonialDto>>(testimonial);
            return View(dto);
        }

        public async Task<IActionResult> Create(CreateTestimonialDto dto)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    if (dto.ImageUrl != null)
                    {
                        var filePath = Path.Combine("wwwroot/Images/Testimonial", dto.ImageUrl.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await dto.ImageUrl.CopyToAsync(stream);
                        }
                        dto.Image = dto.ImageUrl.FileName;
                    }

                    var entity = _mapper.Map<Testimonial>(dto);
                    await _testimonialDal.AddAsync(entity);

                    TempData["NotifyMessage"] = "Referans verilerimiz başarıyla eklendi!";
                    TempData["NotifyType"] = "success";
                    TempData["NotifyTitle"] = "Ekleme İşlemi Başarılı";

                    // Başarıyla ekledikten sonra liste sayfasına yönlendirme yapabilirsiniz
                    return RedirectToAction("Index");

                }
                catch (Exception)
                {
                    TempData["NotifyMessage"] = "Referans eklenirken bir hata oluştu.";
                    TempData["NotifyType"] = "danger";
                    TempData["NotifyTitle"] = "Ekleme İşleminde Bir Problem Oluştu";
                    return View(dto);
                }
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _testimonialDal.GetByIdAsync(id);

            try
            {
                // Eski görseli kontrol et ve sil
                if (entity.Image != null)
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Testimonial", entity.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                await _testimonialDal.DeleteAsync(entity);
                TempData["NotifyMessage"] = "Referans verileri başarıyla silindi!";
                TempData["NotifyType"] = "success";
                TempData["NotifyTitle"] = "Silme İşlemi Başarılı";

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Referans verileri silinirken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Silme İşleminde Bir Problem Oluştu";
            }

            return RedirectToAction("Index");
        }
    }
}
