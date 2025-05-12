using AkcaUsta.Dtos.ContactDtos;
using AkcaUsta.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.Controllers
{
    public class A_ContactController : Controller
    {
        private readonly IContactDal _contactDal;
        private readonly IMapper _mapper;

        public A_ContactController(IContactDal contactDal, IMapper mapper)
        {
            _contactDal = contactDal;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var contact = await _contactDal.GetAllAsync();
            var dto = _mapper.Map<List<ResultContactDto>>(contact);
            return View(dto);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _contactDal.GetByIdAsync(id);
            try
            {
                await _contactDal.DeleteAsync(entity);

                // Kullanıcıya bildirim göstermek için TempData kullanabiliriz
                TempData["NotifyMessage"] = "Gelen mesaj bilgileri başarıyla silindi!";
                TempData["NotifyType"] = "success"; // Diğer seçenekler: info, warning, danger
                TempData["NotifyTitle"] = "Silme İşlemi Başarılı";  // Başlık burada!

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "Gelen mesaj bilgileri silinirken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Silme İşleminde Bir Problem Oluştu";  // Başlık burada!

            }
            return RedirectToAction("Index");
        }
    }
}
