namespace AkcaUsta.Dtos.ServiceDtos
{
    public class ResultServiceDto
    {
        public int ServiceID { get; set; }
        public string Icon { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
