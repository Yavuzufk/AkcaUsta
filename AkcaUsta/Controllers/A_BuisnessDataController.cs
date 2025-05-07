using AkcaUsta.Dtos.BuisnessDataDtos;
using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.Controllers
{
    public class A_BuisnessDataController : Controller
    {
        private readonly IBuisnessDataDAl _buisnessDataDAl;
        private readonly IMapper _mapper;

        public A_BuisnessDataController(IBuisnessDataDAl buisnessDataDAl, IMapper mapper)
        {
            _buisnessDataDAl = buisnessDataDAl;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var buisnessData = (await _buisnessDataDAl.GetAllAsync()).FirstOrDefault();
            var result = _mapper.Map<ResultBuisnessDataDto>(buisnessData);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ResultBuisnessDataDto model)
        {
            try
            {
                // İş verilerini eklemek için repository metodunu çağırıyoruz
                await _buisnessDataDAl.AddToExistingDataAsync(model);

                // Kullanıcıya bildirim göstermek için TempData kullanabiliriz
                TempData["NotifyMessage"] = "İş verilerimiz başarıyla eklendi!";
                TempData["NotifyType"] = "success"; // Diğer seçenekler: info, warning, danger
                TempData["NotifyTitle"] = "Ekleme İşlemi Başarılı";  // Başlık burada!
            }
            catch (Exception)
            {
                TempData["NotifyMessage"] = "İş verilerimiz eklenirken bir hata oluştu.";
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Ekleme İşleminde Bir Problem Oluştu";  // Başlık burada!
            }


            return RedirectToAction("Index");
        }
    }
}
