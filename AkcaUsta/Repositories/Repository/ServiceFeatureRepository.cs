using AkcaUsta.Context;
using AkcaUsta.Entity;
using AkcaUsta.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AkcaUsta.Repositories.Repository
{
    public class ServiceFeatureRepository : GenericRepository<ServiceFeature>, IServiceFeatureDal
    {
        public ServiceFeatureRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<ServiceFeature>> GetFeaturesByServiceID(int ServiceID)
        {
            using var _context = new AppDbContext();
            return await _context.ServiceFeatures
                                 .Where(i => i.ServiceId == ServiceID)
                                 .ToListAsync();
        }
    }
}
