using AkcaUsta.Context;
using AkcaUsta.Entity;
using AkcaUsta.Repository.IRepository;

namespace AkcaUsta.Repositories.Repository
{
    public class ServiceRandevuRepository : GenericRepository<ServiceRandevu>, IServiceRandevuDal
    {
        public ServiceRandevuRepository(AppDbContext context) : base(context)
        {
        }
    }
}
