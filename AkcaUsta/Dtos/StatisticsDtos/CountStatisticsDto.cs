namespace AkcaUsta.Dtos.StatisticsDtos
{
    public class CountStatisticsDto
    {
        public int TotalTechnicians { get; set; }
        public int TotalAppointments { get; set; }
        public int TotalMessages { get; set; }
        public string MostPopularService { get; set; } = string.Empty;
    }
}
