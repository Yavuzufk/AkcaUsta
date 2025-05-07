using AkcaUsta.Dtos.AboutDtos;
using AkcaUsta.Entity;
using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.Controllers
{
    public class A_AboutController : Controller
    {
        private readonly IAboutDal _aboutDal;
        private readonly IMapper _mapper;

        public A_AboutController(IAboutDal aboutDal, IMapper mapper)
        {
            _aboutDal = aboutDal;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var about = (await _aboutDal.GetAllAsync()).FirstOrDefault();
            var result = _mapper.Map<ResultAboutDto>(about);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ResultAboutDto dto)
        {
            try
            {
                var about = _mapper.Map<About>(dto);
                await _aboutDal.UpdateAsync(about);
                TempData["NotifyMessage"] = "Hakkımızda bilgisi başarıyla güncellendi!";
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
    }
}
