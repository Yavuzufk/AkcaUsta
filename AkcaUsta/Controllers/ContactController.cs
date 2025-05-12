using AkcaUsta.Dtos.ContactDtos;
using AkcaUsta.Entity;
using AkcaUsta.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AkcaUsta.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactDal _contactDal;
        private readonly IMapper _mapper;

        public ContactController(IContactDal contactDal, IMapper mapper)
        {
            _contactDal = contactDal;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateContactDto dto)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            
            try
            {
                var entity = _mapper.Map<Contact>(dto);
                await _contactDal.AddAsync(entity);

            }
            catch (Exception ex)
            {
                Console.WriteLine("İletişim gönderilirken bir hata oluştu : " + ex);
            }

            return RedirectToAction("Index");
        }
    }
}
