namespace AkcaUsta.Dtos.StatisticsDtos
{
    public class LatestContactDto
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime CreatedDate { get; set; } // Hem tarih hem saat için
    }
}
