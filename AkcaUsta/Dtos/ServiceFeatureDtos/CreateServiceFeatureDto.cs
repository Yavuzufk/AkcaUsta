using AkcaUsta.Entity;

namespace AkcaUsta.Dtos.ServiceFeatureDtos
{
    public class CreateServiceFeatureDto
    {
        public int ServiceId { get; set; }
        public string Feature { get; set; } = null!;
    }
}
