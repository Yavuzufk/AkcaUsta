using AkcaUsta.Context;
using AkcaUsta.Entity;
using AkcaUsta.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AkcaUsta.Repositories.Repository
{
    public class ServiceRepository : GenericRepository<Service>, IServiceDal
    {
        public ServiceRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Service>> GetAllServicesWithFeaturesAsync()
        {
            using var _context = new AppDbContext();

            return await _context.Services
                .Include(s => s.Features)
                .ToListAsync();
        }
    }
}
