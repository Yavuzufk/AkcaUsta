namespace AkcaUsta.Dtos.StatisticsDtos
{
    public class ServiceRandevuStatisticDto
    {
        public string ServiceName { get; set; } = null!;
        public int Count { get; set; }
        public double Percentage { get; set; }
    }
}
