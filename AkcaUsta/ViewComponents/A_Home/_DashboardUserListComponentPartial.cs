using AkcaUsta.Context;
using AkcaUsta.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AkcaUsta.ViewComponents.A_Home
{
    public class _DashboardUserListComponentPartial:ViewComponent
    {
        private readonly UserManager<AppUser> _userManeger;

        public _DashboardUserListComponentPartial(UserManager<AppUser> userManeger)
        {
            _userManeger = userManeger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var users = _userManeger.Users.ToList();
            return View(users);
        }
    }
}
