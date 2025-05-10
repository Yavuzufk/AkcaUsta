using AkcaUsta.Context;
using AkcaUsta.Entity;
using AkcaUsta.Repositories.IRepository;

namespace AkcaUsta.Repositories.Repository
{
    public class TestimonialRepository : GenericRepository<Testimonial>, ITestimonialDal
    {
        public TestimonialRepository(AppDbContext context) : base(context)
        {
        }
    }
}
