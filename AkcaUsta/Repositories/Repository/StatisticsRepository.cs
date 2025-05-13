using AkcaUsta.Context;
using AkcaUsta.Dtos.StatisticsDtos;
using AkcaUsta.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AkcaUsta.Repositories.Repository
{
    public class StatisticsRepository : IStatisticsDal
    {
        private readonly AppDbContext _context;

        public StatisticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LatestContactDto>> GetLatestContactsAsync(int count = 5)
        {
            return await _context.Contacts
                .OrderByDescending(x => x.CreatedDate)
                .Take(count)
                .Select(x => new LatestContactDto
                {
                    Name = x.Name,
                    Phone = x.Phone,
                    CreatedDate = x.CreatedDate
                })
                .ToListAsync();
        }

        public async Task<List<ServiceRandevuStatisticDto>> GetServiceStatisticsAsync()
        {
            var totalCount = await _context.ServiceRandevus.CountAsync();

            var serviceStats = await _context.ServiceRandevus
                .Where(x => !string.IsNullOrEmpty(x.Service))
                .GroupBy(x => x.Service)
                .Select(g => new ServiceRandevuStatisticDto
                {
                    ServiceName = g.Key,
                    Count = g.Count(),
                    Percentage = totalCount == 0 ? 0 : Math.Round((double)g.Count() * 100 / totalCount, 2)
                })
                .ToListAsync();

            return serviceStats;
        }

        public async Task<CountStatisticsDto> GetStatisticsAsync()
        {
            var totalTechnicians = await _context.Technicians.CountAsync();
            var totalAppointments = await _context.ServiceRandevus.CountAsync();
            var totalMessages = await _context.Contacts.CountAsync();

            var mostPopularService = await _context.ServiceRandevus
                .Where(sr => !string.IsNullOrEmpty(sr.Service))
                .GroupBy(sr => sr.Service)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync() ?? "Veri yok";

            return new CountStatisticsDto
            {
                TotalTechnicians = totalTechnicians,
                TotalAppointments = totalAppointments,
                TotalMessages = totalMessages,
                MostPopularService = mostPopularService
            };
        }
    }
}
