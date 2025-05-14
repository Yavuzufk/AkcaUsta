using AkcaUsta.Dtos.AppUserDtos;
using AkcaUsta.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RegisterController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["NotifyMessage"] = "Lütfen tüm alanları doğru şekilde doldurunuz !";
                TempData["NotifyType"] = "warning"; 
                TempData["NotifyTitle"] = "Kullanıcı Kayıt İşlemi Başarısız"; 

                return RedirectToAction("Index", "A_Home");
            }

            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                // Ekstra alanlar
                Name = dto.Name,
                Surname = dto.Surname,
                Age = dto.Age,
                ShortDescription = dto.ShortDescription
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                if (dto.Roles != null && dto.Roles.Any())
                {
                    foreach (var role in dto.Roles)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                }

                TempData["NotifyMessage"] = "Hoşgeldi! Kullanıcı başarılı bir şekilde kaydedilmiştir.";
                TempData["NotifyType"] = "success"; 
                TempData["NotifyTitle"] = "Kullanıcı Kayıt İşlemi Başarılı";
            }
            else
            {
                TempData["NotifyMessage"] = string.Join("<br/>", result.Errors.Select(e => e.Description));
                TempData["NotifyType"] = "danger";
                TempData["NotifyTitle"] = "Hata !"; 
            }

            return RedirectToAction("Index", "A_Home");
        }
    }
}
