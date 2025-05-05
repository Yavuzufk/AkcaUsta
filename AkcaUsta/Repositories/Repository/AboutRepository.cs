using AkcaUsta.Context;
using AkcaUsta.Entity;
using AkcaUsta.Repository.IRepository;

namespace AkcaUsta.Repositories.Repository
{
    public class AboutRepository:GenericRepository<About>,IAboutDal
    {
        public AboutRepository(AppDbContext context) : base(context)
        {
        }
    }
}
