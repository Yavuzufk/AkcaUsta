using AkcaUsta.Dtos.TestimonialDtos;
using AkcaUsta.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AkcaUsta.ViewComponents.HomepageComponents
{
    public class _TestimonialComponentPartial:ViewComponent
    {
        private readonly ITestimonialDal _testimonialDal;
        private readonly IMapper _mapper;

        public _TestimonialComponentPartial(ITestimonialDal testimonialDal, IMapper mapper)
        {
            _testimonialDal = testimonialDal;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var testimonial = await _testimonialDal.GetAllAsync();
            var testimonialDtos = _mapper.Map<List<ResultTestimonialDto>>(testimonial);
            return View(testimonialDtos);
        }
    }
}
