using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AkcaUsta.Filters
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Kullanıcının kimliği doğrulanmamışsa ya da admin değilse giriş sayfasına yönlendir
            if (!user.Identity.IsAuthenticated || !user.IsInRole("Admin"))
            {
                context.Result = new RedirectToActionResult("Index", "Login", null); // Giriş sayfasına yönlendir
            }
        }
    }
}
