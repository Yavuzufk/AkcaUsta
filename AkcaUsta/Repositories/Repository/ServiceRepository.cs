using AkcaUsta.Context;
using AkcaUsta.Entity;
using AkcaUsta.Repository.IRepository;

namespace AkcaUsta.Repositories.Repository
{
    public class ServiceRepository : GenericRepository<Service>, IServiceDal
    {
        public ServiceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
