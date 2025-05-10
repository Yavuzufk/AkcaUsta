namespace AkcaUsta.Dtos.TechnicianDtos
{
    public class UpdateTechnicianDto
    {
        public int TechnicianID { get; set; }
        public string FullNanme { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Job { get; set; } = null!;
    }
}
