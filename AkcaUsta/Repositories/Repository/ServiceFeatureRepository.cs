using AkcaUsta.Context;
using AkcaUsta.Entity;
using AkcaUsta.Repository.IRepository;

namespace AkcaUsta.Repositories.Repository
{
    public class ServiceFeatureRepository : GenericRepository<ServiceFeature>, IServiceFeatureDal
    {
        public ServiceFeatureRepository(AppDbContext context) : base(context)
        {
        }
    }
}
