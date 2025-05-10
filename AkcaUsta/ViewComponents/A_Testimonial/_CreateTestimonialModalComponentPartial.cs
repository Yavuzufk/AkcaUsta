using AkcaUsta.Dtos.TestimonialDtos;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents.A_Testimonial
{
    public class _CreateTestimonialModalComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewmodel = new CreateTestimonialDto();
            return View(viewmodel);
        }
    }
}
