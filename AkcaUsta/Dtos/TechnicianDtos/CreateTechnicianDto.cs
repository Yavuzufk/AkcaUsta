namespace AkcaUsta.Dtos.TechnicianDtos
{
    public class CreateTechnicianDto
    {
        public string FullNanme { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Job { get; set; } = null!;
        public IFormFile ImageUrl { get; set; } = null!;

    }
}
