using AkcaUsta.Context;
using AkcaUsta.Dtos.ReportDtos;
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

        public async Task<BusinessSummaryDto> GetBusinessSummaryReport()
        {
            using var _context = new AppDbContext();

            var randevular = await _context.ServiceRandevus.ToListAsync();

            var total = randevular.Count;

            // Hizmet dağılımı: string'e göre gruplanıyor
            var hizmetDagilimi = randevular
                .GroupBy(r => r.Service)  // Service zaten string
                .ToDictionary(g => g.Key ?? "Bilinmiyor", g => g.Count());

            // En çok tercih edilen hizmet
            var mostPopularService = hizmetDagilimi
                .OrderByDescending(x => x.Value)
                .FirstOrDefault().Key ?? "Yok";

            // Gün dağılımı
            var gunDagilimi = randevular
                .GroupBy(r => r.Date.DayOfWeek.ToString())
                .ToDictionary(g => g.Key, g => g.Count());

            // En yoğun gün
            var mostPopularDay = gunDagilimi
                .OrderByDescending(x => x.Value)
                .FirstOrDefault().Key ?? "Yok";

            return new BusinessSummaryDto
            {
                TotalAppointments = total,
                AppointmentsByService = hizmetDagilimi,
                MostPopularService = mostPopularService,
                MostPopularDay = mostPopularDay
            };

        }

        public  async Task<List<ServiceRandevu>> GetLastMonthRandevus()
        {
            using var _context = new AppDbContext();

            var oneMonthAgo = DateTime.Now.AddMonths(-1);
            return await _context.ServiceRandevus
                                 .Where(x => x.Date >= oneMonthAgo)
                                 .ToListAsync();
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

        public async Task<RandevuStatisticsDto> GetRandevuStatistics()
        {
            using var _context = new AppDbContext();

            var randevular = await _context.ServiceRandevus.ToListAsync();

            var totalCount = randevular.Count;
            var confirmedCount = randevular.Count(r => r.RandevuStatus == true);
            var pendingCount = randevular.Count(r => r.RandevuStatus == false);

            return new RandevuStatisticsDto
            {
                TotalCount = totalCount,
                ConfirmedCount = confirmedCount,
                PendingCount = pendingCount
            };
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
