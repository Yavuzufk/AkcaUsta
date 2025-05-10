namespace AkcaUsta.Dtos.TestimonialDtos
{
    public class CreateTestimonialDto
    {
        public string Name { get; set; } = null!;
        public string Service { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int? Stars { get; set; }
        public IFormFile ImageUrl { get; set; } = null!;
    }
}
