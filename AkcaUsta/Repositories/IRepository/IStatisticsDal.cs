using AkcaUsta.Dtos.StatisticsDtos;

namespace AkcaUsta.Repositories.IRepository
{
    public interface IStatisticsDal
    {
        Task<CountStatisticsDto> GetStatisticsAsync();
        Task<List<ServiceRandevuStatisticDto>> GetServiceStatisticsAsync();
        Task<List<LatestContactDto>> GetLatestContactsAsync(int count = 5);



    }
}
