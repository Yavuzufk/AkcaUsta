namespace AkcaUsta.Dtos.TestimonialDtos
{
    public class ResultTestimonialDto
    {
        public int TestimonialID { get; set; }
        public string Name { get; set; } = null!;
        public string Service { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int? Stars { get; set; }
    }
}
