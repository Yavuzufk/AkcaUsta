using AkcaUsta.Context;
using AkcaUsta.Entity;
using AkcaUsta.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AkcaUsta.Repositories.Repository
{
    public class ServiceRandevuRepository : GenericRepository<ServiceRandevu>, IServiceRandevuDal
    {
        public ServiceRandevuRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<ServiceRandevu>> GetPassiveRandevuAsync()
        {
            using var _context = new AppDbContext();

            return await _context.ServiceRandevus
                     .Where(x => x.RandevuStatus == false)
                     .ToListAsync();
        }

        public async Task<int> GetPendingRandevuCountAsync()
        {
            using var _context = new AppDbContext();

            return await _context.ServiceRandevus
                     .Where(x => x.RandevuStatus == false)
                     .CountAsync();
        }

        public async Task ToggleRandevuStatusAsync(int id)
        {
            using var _context = new AppDbContext();
            var randevu = await _context.ServiceRandevus.FindAsync(id);
            if (randevu == null) return;

            randevu.RandevuStatus = !randevu.RandevuStatus; // bool için sade ve doğru yol

            _context.ServiceRandevus.Update(randevu);
            await _context.SaveChangesAsync();
        }
    }
}
