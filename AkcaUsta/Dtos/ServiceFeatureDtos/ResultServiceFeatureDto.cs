using AkcaUsta.Entity;

namespace AkcaUsta.Dtos.ServiceFeatureDtos
{
    public class ResultServiceFeatureDto
    {
        public int ServiceFeatureID { get; set; }
        public int ServiceId { get; set; }
        public string Feature { get; set; } = null!;
    }
}
