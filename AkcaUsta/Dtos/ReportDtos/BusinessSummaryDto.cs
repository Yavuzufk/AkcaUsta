namespace AkcaUsta.Dtos.ReportDtos
{
    public class BusinessSummaryDto
    {
        public int TotalAppointments { get; set; }
        public Dictionary<string, int> AppointmentsByService { get; set; }
        public string MostPopularService { get; set; }
        public string MostPopularDay { get; set; }
    }
}
