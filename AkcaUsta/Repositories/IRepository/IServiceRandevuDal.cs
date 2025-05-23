﻿using AkcaUsta.Dtos.ReportDtos;
using AkcaUsta.Entity;

namespace AkcaUsta.Repository.IRepository
{
    public interface IServiceRandevuDal: IGenericDal<ServiceRandevu>
    {
        Task ToggleRandevuStatusAsync(int id);
        Task<int> GetPendingRandevuCountAsync();
        Task<List<ServiceRandevu>> GetPassiveRandevuAsync();
        Task<List<ServiceRandevu>> GetLastMonthRandevus();
        Task<RandevuStatisticsDto> GetRandevuStatistics();
        Task<BusinessSummaryDto> GetBusinessSummaryReport();



    }
}
